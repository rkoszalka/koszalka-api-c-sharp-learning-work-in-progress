using System;
using Confluent.Kafka;
using koszalka_api.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KafkaProducerController : Controller
    {
        private readonly ProducerKafka _producerKafka;

        public KafkaProducerController(ProducerKafka producerKafka)
        {
            _producerKafka = producerKafka;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public string Post([FromQuery] string msg, string topic)
        {
            return _producerKafka.SendMessageByKafka(msg, topic);
        }
    }
}
