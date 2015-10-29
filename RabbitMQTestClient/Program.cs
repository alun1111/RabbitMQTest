using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQTestClient
{
    class Program
    {
        private static IModel _channel;
        private static string _exchangeName = "TestExchange";
        private static string _queueName = "TestQueue";
        private static string _routingKey = "98hf9rhr947hrf48hoial34h";

        public static void Main()
        {
            _channel = AmqpConnection.OpenChannel();

            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
            _channel.QueueDeclare(_queueName, false, false, false, null);
            _channel.QueueBind(_queueName, _exchangeName, _routingKey, null);

            var t = new Timer(TimerCallback, _channel, 0, 2000);

            Console.ReadLine();
        }

        private static void TimerCallback(Object o)
        {
            Console.WriteLine("Sending message: " + DateTime.Now);
            IModel model = (IModel)o;

            lock (model)
            {
                byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Time: {0}", DateTime.Now.ToLongTimeString()));
                model.BasicPublish(_exchangeName, _routingKey, null, messageBodyBytes);
            }

            GC.Collect();
        }

    }
}
