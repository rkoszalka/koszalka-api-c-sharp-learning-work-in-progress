using System;
using Confluent.Kafka;
using koszalka_api.Events.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KafkaProducerController : Controller
    {
        private readonly KafkaProducer _kafkaProducer;
        private readonly IConfiguration _configuration;

        public KafkaProducerController(KafkaProducer kafkaProducer, IConfiguration configuration)
        {
            _kafkaProducer = kafkaProducer;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public string Post([FromQuery] string msg)
        {
            CancellationTokenSource cts = new();
            KafkaConsumer kafkaConsumer = new(_configuration);
            kafkaConsumer.Run_Consume(_configuration.GetSection("Kafka:BootstrapServers").Value, _configuration.GetSection("Kafka:Topic").Value.Split(',').ToList(), cts.Token);
            return _kafkaProducer.SendMessageByKafka(msg);
        }
    }
}
