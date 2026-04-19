using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using AmericanAirlinesApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> PostReserva(Reserva reserva)
    {
        // 1. Busca o voo e a aeronave vinculada para verificar a capacidade
        var voo = await _context.Voos
            .Include(v => v.Aeronave)
            .FirstOrDefaultAsync(v => v.Id == reserva.VooId);

        if (voo == null)
        {
            return NotFound("Voo não encontrado.");
        }

        // 2. Validação de Overbooking (Checkpoint 3 da lista)
        var totalReservas = await _context.Reservas.CountAsync(r => r.VooId == reserva.VooId);
        
        if (voo.Aeronave != null && totalReservas >= voo.Aeronave.CapacidadePassageiros)
        {
            return BadRequest("Voo lotado. Não é possível realizar novas reservas para este voo.");
        }

        // 3. Lógica de Assento na Janela (Regra da página 3)
        if (!string.IsNullOrEmpty(reserva.Assento))
        {
            if (reserva.Assento.EndsWith("A") || reserva.Assento.EndsWith("F"))
            {
                Console.WriteLine("Assento na janela reservado com sucesso!");
            }
        }

        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
        
        return Ok(reserva);
    }

    [HttpGet]
    public async Task<IActionResult> GetReservas()
    {
        var reservas = await _context.Reservas.Include(r => r.Voo).ToListAsync();
        return Ok(reservas);
    }
}