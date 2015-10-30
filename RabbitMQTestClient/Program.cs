using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQTestShared;

namespace RabbitMQTestClient
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
                _channel.ExchangeDeclare(_settings.exchangeName, ExchangeType.Direct);
                _channel.QueueDeclare(_settings.queueName, false, false, false, null);
                _channel.QueueBind(_settings.queueName, _settings.exchangeName, _settings.routingKey, null);

                var t = new Timer(TimerCallback, _channel, 0, 2000);

                Console.ReadLine();
            }
        }

        private static void TimerCallback(Object o)
        {
            Console.WriteLine("Sending message: " + DateTime.Now);

            IModel model = (IModel)o;

            lock (model)
            {
                byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Time: {0}", DateTime.Now.ToLongTimeString()));

                try
                {
                    model.BasicPublish(_settings.exchangeName, _settings.routingKey, null, messageBodyBytes);
                }
                catch (Exception)
                {
                    
                }
            }

        }

    }
}
