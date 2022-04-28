using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger; //todo: Think something for this

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get([FromBody] Query query)
        {
            return $"Some order with payment Id of {query.PaymentId} and merchant Id of {query.MerchantId}";
        }

        [HttpPost]
        public string Post([FromBody] Order order)
        {
            return "Posted";
        }
    }
}
