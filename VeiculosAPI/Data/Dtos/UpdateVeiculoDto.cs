using System.ComponentModel.DataAnnotations;

namespace VeiculosAPI.Data.Dtos
{
    public class UpdateVeiculoDto
    {

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        public string? Modelo { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "O ano deve estar no formato yyyy")]
        public string? Ano { get; set; }
    }
}
