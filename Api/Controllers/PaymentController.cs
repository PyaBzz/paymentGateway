using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IRequestFactory requestFactory;
        private readonly IResponseFactory responseFactory;

        //doc: Log for auditing
        public PaymentController(IRequestFactory requester, IResponseFactory responser)
        {
            requestFactory = requester;
            responseFactory = responser;
        }

        [HttpGet]
        public string Get([FromBody] Query query)
        {
            var report = ReportFactory.
            return $"Some request with Id of {query.RequestId} and merchant Id of {query.MerchantId}";
        }

        [HttpPost]
        public string Post([FromBody] Dto dto)
        {
            var request = requestFactory.Make(dto);
            var response = responseFactory.Process(request);
            return response;
        }
    }
}
