using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models;
using AmericanAirlinesApi.Data;

namespace AmericanAirlinesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VooController : ControllerBase
{
    private readonly AppDbContext _context;
    public VooController(AppDbContext context) => _context = context;

    [HttpPost]
    public async Task<IActionResult> PostVoo(Voo voo)
    {
        // Verifica se a aeronave está em outro voo que ainda não terminou
        var aeronaveOcupada = await _context.Voos
            .AnyAsync(v => v.AeronaveId == voo.AeronaveId && v.Status == "Em Voo");

        if (aeronaveOcupada)
        {
            return Conflict("Aeronave indisponível, encontra-se em trânsito.");
        }

        _context.Voos.Add(voo);
        await _context.SaveChangesAsync();
        return Ok(voo);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> PatchStatus(int id, [FromBody] string novoStatus)
    {
        var voo = await _context.Voos.FindAsync(id);
        if (voo == null)
        {
            return NotFound();
        }

        // Regra de Ouro: Voo encerrado (Finalizado/Cancelado) não pode voltar para "Em Voo"
        if ((voo.Status == "Finalizado" || voo.Status == "Cancelado") && novoStatus == "Em Voo")
        {
            return BadRequest("Erro de lógica de negócio: Voo encerrado não pode retornar ao status Em Voo.");
        }

        voo.Status = novoStatus;
        await _context.SaveChangesAsync();
        return Ok(voo);
    }
}