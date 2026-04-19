using Microsoft.EntityFrameworkCore;
using AmericanAirlinesApi.Models; // Importante para reconhecer Voo, Reserva, etc.

namespace AmericanAirlinesApi.Data; // O nome após o 'namespace' deve ser exatamente esse

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aeronave> Aeronaves => Set<Aeronave>();
    public DbSet<Voo> Voos => Set<Voo>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Tripulante> Tripulantes => Set<Tripulante>();
}