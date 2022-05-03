
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthcheckController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "OK"; //doc: decide what to return if unhealthy
        }
    }
}