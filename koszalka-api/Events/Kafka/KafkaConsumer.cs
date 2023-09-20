﻿using Confluent.Kafka;
using koszalka_api.Controllers;

namespace koszalka_api.Events.Kafka
{

    

    public class KafkaConsumer
    {
        private readonly IConfiguration _isConfig;

        public KafkaConsumer(IConfiguration isConfig)
        {
            _isConfig = isConfig;
        }

        public void Run_Consume(string broker, List<string> topic, CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = broker,
                GroupId = "csharp-consumer",
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky,
                SaslUsername = _isConfig.GetSection("Kafka:SaslUsername").Value,
                SaslPassword = _isConfig.GetSection("Kafka:SaslPassword").Value,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    Console.WriteLine(
                        "Partitions incrementally assigned: [" +
                        string.Join(',', partitions.Select(p => p.Partition.Value)) +
                        "], all: [" +
                        string.Join(',', c.Assignment.Concat(partitions).Select(p => p.Partition.Value)) +
                        "]");

                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    var remaining = c.Assignment.Where(atp => partitions.Where(rtp => rtp.TopicPartition == atp).Count() == 0);
                    Console.WriteLine(
                        "Partitions incrementally revoked: [" +
                        string.Join(',', partitions.Select(p => p.Partition.Value)) +
                        "], remaining: [" +
                        string.Join(',', remaining.Select(p => p.Partition.Value)) +
                        "]");
                })
                .SetPartitionsLostHandler((c, partitions) =>
                {
                    Console.WriteLine($"Partitions were lost: [{string.Join(", ", partitions)}]");
                })
                .Build())
            {
                consumer.Subscribe(topic);

                try
                {
                    var consumeResult = consumer.Consume(cancellationToken);
                    Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Closing consumer.");
                    consumer.Close();
                }
            }
        }
    }
}
