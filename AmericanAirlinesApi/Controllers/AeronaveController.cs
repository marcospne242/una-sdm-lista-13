using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using AmericanAirlinesApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AeronaveController : ControllerBase
{
    private readonly AppDbContext _context;

    public AeronaveController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Aeronave aeronave)
    {
        _context.Aeronaves.Add(aeronave);
        await _context.SaveChangesAsync();
        return Ok(aeronave);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Aeronaves.ToListAsync());
    }
}