using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmericanAirlinesApi.Models;

public class Tripulante 
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Função { get; set; } = string.Empty; // [Piloto, Copiloto, Comissário]
    public string NumeroLicenca { get; set; } = string.Empty;
}
