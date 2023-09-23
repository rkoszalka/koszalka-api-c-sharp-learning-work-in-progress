using RabbitMQ.Client;

namespace koszalka_api.Events.RabbitMQ.Interfaces
{
    public interface IRabbitMQConnectionFactory
    {
        IConnection CreateConnection();
    }
}
