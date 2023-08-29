using Microsoft.AspNetCore.Mvc;
using VeiculosAPI.Models;

namespace VeiculosAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private static List<Veiculo> veiculos = new List<Veiculo>();
    private static int id = 0;

    [HttpPost]
    public IActionResult AdicionaVeiculo([FromBody] Veiculo veiculo) //From body vendo do corpo da requisição // Padrão REST aplicado
    {
        veiculo.Id = id++;
        veiculos.Add(veiculo);
        return CreatedAtAction(nameof(RecuperaVeiculoPorId), // Padrão REST aplicado
            new { id = veiculo.Id }, 
            veiculo);
    }

    [HttpGet]
    public IEnumerable<Veiculo> RecuperaVeiculos([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return veiculos.Skip(skip).Take(take); // Escolha a faixa de veiculos a ser selecionada!!
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaVeiculoPorId(int id) // IActionResult é padrão REST
    {
        var veiculo = veiculos.FirstOrDefault(veiculo => veiculo.Id == id);
        if (veiculo == null) return NotFound();
        return Ok(veiculo);
    }
}
