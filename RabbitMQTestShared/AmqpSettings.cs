using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQTestShared
{
    public class AmqpSettings
    {
        public string user = "testuser";
        public string password = "testpassword";
        public string hostName = "10.128.13.9";
        public string exchangeName = "TestExchange";
        public string queueName = "TestQueue";
        public string routingKey = "98hf9rhr947hrf48hoial34h";
    }
}
