using koszalka_api.Events.RabbitMQ.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Configuration;
using System.Text;

namespace koszalka_api.RabbitMQ
{
    public class RabbitMQProducer : IRabitMQProducer
    {
        private readonly IRabbitMQConnectionFactory _connectionFactory;

        public RabbitMQProducer(IRabbitMQConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void SendProductMessage<T>(T message)
        {
            var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "product", body: body);

        }
    }
}
