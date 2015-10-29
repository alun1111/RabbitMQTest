using System;
using System.Collections;
using RabbitMQ.Client;

namespace RabbitMQTestShared
{
    public class AmqpConnection : IDisposable
    {
        private IConnection _connect;
        private IModel _channel;


        public IModel Connect(AmqpSettings settings)
        {
            var factory = new ConnectionFactory
            {
                UserName = settings.user
                ,
                Password = settings.password
                ,
                HostName = settings.hostName
            };

            _connect = factory.CreateConnection();

            _channel = _connect.CreateModel();

            _channel.ExchangeDeclare(settings.hostName, ExchangeType.Direct);
            _channel.QueueDeclare(settings.queueName, false, false, false, null);
            _channel.QueueBind(settings.queueName, settings.exchangeName, settings.routingKey, null);

            return _channel;

        }

        public void Dispose()
        {
            _connect.Dispose();
        }
    }
}