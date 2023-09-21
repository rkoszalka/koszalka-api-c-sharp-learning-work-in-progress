using System;
using Confluent.Kafka;
using koszalka_api.Events.RabbitMQ;
using koszalka_api.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace koszalka_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitMQController : Controller
    {
        private readonly IRabitMQProducer _rabitMQProducer;
        private readonly IRabbitMQConsumer _rabbitMqConsumer;

        public RabbitMQController(IRabitMQProducer iRabitMqProducer, IRabbitMQConsumer rabbitMqConsumer)
        {
            _rabitMQProducer = iRabitMqProducer;
            _rabbitMqConsumer = rabbitMqConsumer;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public string Post([FromQuery] string msg)
        {
            // _rabbitMqConsumer.CreateRabbitMQConsumer();
            _rabitMQProducer.SendProductMessage(msg);
            return msg;
        }

    }
}
