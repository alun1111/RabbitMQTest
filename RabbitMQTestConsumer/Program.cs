using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (ch, ea) =>
                {
                    var body = ea.Body;
                    // ... process the message
                    _channel.BasicAck(ea.DeliveryTag, false);
                };

                String consumerTag = _channel.BasicConsume(_settings.queueName, false, consumer);

                Console.WriteLine(consumerTag);
                Console.ReadLine();
            }

        }
    }
}
