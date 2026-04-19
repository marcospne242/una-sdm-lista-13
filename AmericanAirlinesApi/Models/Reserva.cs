using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmericanAirlinesApi.Models
{
public class Reserva {
    public int Id { get; set; }
    public int VooId { get; set; }
    public Voo? Voo { get; set; }
    public string NomePassageiro { get; set; } = string.Empty;
    public string Assento { get; set; } = string.Empty;
}
}