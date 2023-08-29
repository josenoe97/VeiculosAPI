using Microsoft.AspNetCore.Mvc;
using VeiculosAPI.Data;
using VeiculosAPI.Models;

namespace VeiculosAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private VeiculoContext _context;

    public VeiculoController(VeiculoContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AdicionaVeiculo([FromBody] Veiculo veiculo) //From body vendo do corpo da requisição // Padrão REST aplicado
    {
        _context.Veiculos.Add(veiculo);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaVeiculoPorId), // Padrão REST aplicado
            new { id = veiculo.Id }, 
            veiculo);
    }

    [HttpGet]
    public IEnumerable<Veiculo> RecuperaVeiculos([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Veiculos.Skip(skip).Take(take); // Escolha a faixa de veiculos a ser selecionada!!
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaVeiculoPorId(int id) // IActionResult é padrão REST
    {
        var veiculo = _context.Veiculos.FirstOrDefault(veiculo => veiculo.Id == id);
        if (veiculo == null) return NotFound();
        return Ok(veiculo);
    }
}
