using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("")]
    [ApiController]
    public class HealthcheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Healthcheck()
        {
            return StatusCode(StatusCodes.Status200OK, "API is running");
        }
    }
}
