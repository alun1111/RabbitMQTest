using RabbitMQ.Client;

namespace RabbitMQTestClient
{
    internal static class AmqpConnection
    {
        private static string _uri = "amqp://user:pass@hostName:port/vhost";

        private static IConnection Connect()
        {
            var factory = new ConnectionFactory {Uri = _uri};

            IConnection conn = factory.CreateConnection();

            return conn;
        }

        public static IModel OpenChannel()
        {
            var conn = Connect();

            return conn.CreateModel();
        }

    }
}