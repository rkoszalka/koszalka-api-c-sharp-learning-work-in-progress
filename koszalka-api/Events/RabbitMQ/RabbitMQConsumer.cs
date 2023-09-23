using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using koszalka_api.Events.RabbitMQ.Interfaces;
using Microsoft.AspNetCore.Connections;

namespace koszalka_api.RabbitMQ
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly IRabbitMQConnectionFactory _connectionFactory;

        public RabbitMQConsumer(IRabbitMQConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void CreateRabbitMQConsumer()
        {
            var connection = _connectionFactory.CreateConnection();
            if (connection.IsOpen)
            {
                Console.WriteLine($"Consumer started: {connection.IsOpen}");
            }
            using
                var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Product message received: {message}");
            };
            channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
        }
    }
}
