using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("healthcheck")]
    public class HealthController : ControllerBase
    {
        public HealthController()
        {

        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok("Service is healthy!");
        }
    }
}
