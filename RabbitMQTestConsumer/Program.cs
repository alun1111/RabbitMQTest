using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQTestShared;

namespace RabbitMQTestConsumer
{
    class Program
    {
        private static IModel _channel;
        private static AmqpSettings _settings;

        public static void Main()
        {
            _settings = new AmqpSettings();

            using (var amqp = new AmqpConnection())
            using (_channel = amqp.Connect(_settings))
            {
                Subscription sub = new Subscription(_channel, _settings.queueName);
                foreach (BasicDeliverEventArgs e in sub)
                {
                    var asText = Encoding.UTF8.GetString(e.Body);
                    Console.WriteLine(asText);

                    // Must acknowledge
                    sub.Ack(e);
                }

                Console.ReadLine();
            }

        }
    }
}
