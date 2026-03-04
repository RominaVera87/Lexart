using Assets.Infrastructure.Native;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Api.Controllers;

[ApiController]
[Route("api/native")]
public class NativeTestController : ControllerBase
{
    [HttpGet("healthscore")]
    public ActionResult<int> GetHealthScore([FromQuery] int temperature = 70, [FromQuery] int vibration = 10, [FromQuery] int pressure = 30)
    {
        var score = HealthScoreNative.CalculateHealthScore(temperature, vibration, pressure);
        return Ok(score);
    }
}