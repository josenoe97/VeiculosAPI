using System.ComponentModel.DataAnnotations;

namespace VeiculosAPI.Data.Dtos;

public class ReadVeiculoDto
{
    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? Ano { get; set; }

    public DateTime HoraConsulta { get; set; } = DateTime.Now;
}
