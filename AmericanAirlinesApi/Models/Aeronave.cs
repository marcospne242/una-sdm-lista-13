using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmericanAirlinesApi.Models
{
public class Aeronave {
    public int Id { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public string CodigoCauda { get; set; } = string.Empty;
    public int CapacidadePassageiros { get; set; }
}
}