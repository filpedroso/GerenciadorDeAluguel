using Microsoft.AspNetCore.Mvc;
using GerenciadorDeAluguel.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeAluguel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    
    public HealthController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Health check - verifies API and database are running
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        // Test database connectivity
        var canConnect = await _dbContext.Database.CanConnectAsync();
        
        return Ok(new
        {
            Status = "Healthy",
            DatabaseConnected = canConnect,
            Timestamp = DateTime.UtcNow,
            Tables = new[]
            {
                "Clients",
                "Properties", 
                "Reservations"
            }
        });
    }
}
