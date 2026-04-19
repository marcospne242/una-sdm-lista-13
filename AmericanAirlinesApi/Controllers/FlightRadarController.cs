using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using AmericanAirlinesApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/radar")]
public class FlightRadarController : ControllerBase 
{
    private readonly AppDbContext _context;
    public FlightRadarController(AppDbContext context) => _context = context;

    [HttpGet("proximos-destinos")]
    public IActionResult GetProximosDestinos() 
    {
        Thread.Sleep(2000); // Simulação de latência de satélite [cite: 78]
        
        var destinos = _context.Voos
            .Where(v => v.Status == "Em Voo" || v.Status == "Agendado")
            .GroupBy(v => v.Destino)
            .Select(g => new { Destino = g.Key, Quantidade = g.Count() })
            .ToList();
        
        return Ok(destinos);
    }
}