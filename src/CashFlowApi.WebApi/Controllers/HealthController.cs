using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlowApi.WebApi.Controllers;

[ApiController]
[Route("api/health")]
[Produces("application/json")]
public class HealthController : Controller
{
    [HttpGet("")]
    [SwaggerOperation(Summary = "Check if the api is alive.")]
    [SwaggerResponse(StatusCodes.Status200OK, "The api is health.")]
    public IActionResult Health()
    {
        return Ok( new { status = "Alive" });
    }
}
