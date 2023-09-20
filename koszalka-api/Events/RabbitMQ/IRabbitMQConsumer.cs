namespace koszalka_api.Events.RabbitMQ
{
    public interface IRabbitMQConsumer
    {
        public void CreateRabbitMQConsumer(WebApplication app);
    }
}