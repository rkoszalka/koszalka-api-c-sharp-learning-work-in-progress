using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Configuration;
using System.Text;

namespace koszalka_api.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        private readonly IConfiguration _configuration;

        public RabitMQProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendProductMessage<T>(T message)
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
            using
                var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);
        }
    }
}
