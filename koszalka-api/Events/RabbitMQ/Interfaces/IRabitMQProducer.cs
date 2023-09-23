namespace koszalka_api.Events.RabbitMQ.Interfaces
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
