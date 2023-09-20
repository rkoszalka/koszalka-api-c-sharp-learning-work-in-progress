using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace koszalka_api.RabbitMQ
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        public void CreateRabbitMQConsumer(WebApplication app)
        {
            // @todo: use IConfiguration
            var factory = new ConnectionFactory
            {
                HostName = "host.docker.internal",
                Port = 207,
                UserName = "guest",
                Password  = "guest",
                VirtualHost = "/",
            };
            Console.WriteLine($"Factory: {factory}");
            var connection = factory.CreateConnection();
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
            app.Run();
            Console.Read();
        }
    }
}
