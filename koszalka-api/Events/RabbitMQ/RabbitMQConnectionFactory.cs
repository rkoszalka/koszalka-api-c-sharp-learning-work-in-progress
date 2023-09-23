using koszalka_api.Events.RabbitMQ.Interfaces;
using RabbitMQ.Client;

namespace koszalka_api.Events.RabbitMQ
{
    public class RabbitMQConnectionFactory : IRabbitMQConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public RabbitMQConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnection CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetSection("RabbitMQ:Host").Value,
                Port = int.Parse(_configuration.GetSection("RabbitMQ:Port").Value),
                UserName = _configuration.GetSection("RabbitMQ:UserName").Value,
                Password = _configuration.GetSection("RabbitMQ:Password").Value,
                VirtualHost = _configuration.GetSection("RabbitMQ:VirtualHost").Value
            };
            var connection = factory.CreateConnection();
            return connection;
        }
    }
}
