using Microsoft.AspNetCore.Mvc;

namespace Hypesoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    /// <summary>
    /// Verifica se a API está em execução.
    /// </summary>
    /// <returns>Uma mensagem de sucesso se a API estiver online.</returns>
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("API is running.");
    }
}