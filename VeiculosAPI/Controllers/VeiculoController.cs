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
    public void AdicionaVeiculo([FromBody] Veiculo veiculo) //From body vendo do corpo da requisição
    {
        veiculo.Id = id++;
        veiculos.Add(veiculo);
        Console.WriteLine(veiculo.Marca);
        Console.WriteLine(veiculo.Modelo);
        Console.WriteLine(veiculo.Ano);
    }

    [HttpGet]
    public IEnumerable<Veiculo> RecuperaVeiculos()
    {
        return veiculos;
    }

    [HttpGet("{id}")]
    public Veiculo? RecuperaVeiculoPorId(int id)
    {
        return veiculos.FirstOrDefault(veiculo => veiculo.Id == id);
    }
}
