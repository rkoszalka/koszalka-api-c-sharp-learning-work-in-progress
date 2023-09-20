using Confluent.Kafka;

namespace koszalka_api.Events.Kafka
{
    public class KafkaProducer
    {
        public readonly IConfiguration _icConfig;

        public KafkaProducer(IConfiguration icConfig)
        {
            _icConfig = icConfig;
        }

        public string SendMessageByKafka(string message)
        {
            string topic = _icConfig.GetSection("Kafka:Topic").Value;
            var config = new ProducerConfig
            {
                BootstrapServers = _icConfig.GetSection("Kafka:BootstrapServers").Value,
                SaslUsername = _icConfig.GetSection("Kafka:SaslUsername").Value,
                SaslPassword = _icConfig.GetSection("Kafka:SaslPassword").Value,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var sendResult = producer
                        .ProduceAsync(topic, new Message<Null, string> { Value = message })
                        .GetAwaiter()
                        .GetResult();

                    return $"Mensagem '{sendResult.Value}' de '{sendResult.TopicPartitionOffset}'";
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }

            return string.Empty;
        }


    }
}
