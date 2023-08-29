using System.ComponentModel.DataAnnotations;

namespace VeiculosAPI.Models;

public class Veiculo
{
    /*[Key]
    public int Id { get; set; }*/

    [Required(ErrorMessage = "{0} é obrigatório")]
    public string Marca { get; set; }

    [Required(ErrorMessage = "{0} é obrigatório")]
    public string Modelo { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "O ano deve estar no formato yyyy")]
    public string Ano { get; set; }

}
