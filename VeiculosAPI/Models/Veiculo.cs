using System.ComponentModel.DataAnnotations;

namespace VeiculosAPI.Models;

public class Veiculo
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Marca { get; set; }

    [Required]
    public string Modelo { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:yyyy}")]
    public DateTime Ano { get; set; }

}
