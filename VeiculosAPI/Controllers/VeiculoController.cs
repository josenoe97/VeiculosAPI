using Microsoft.AspNetCore.Mvc;
using VeiculosAPI.Models;

namespace VeiculosAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private static List<Veiculo> veiculos = new List<Veiculo>();

    [HttpPost]
    public void AdicionaVeiculo([FromBody] Veiculo veiculo) //From body vendo do corpo da requisição
    {
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
}
