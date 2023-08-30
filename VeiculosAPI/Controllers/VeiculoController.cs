using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VeiculosAPI.Data;
using VeiculosAPI.Data.Dtos;
using VeiculosAPI.Models;

namespace VeiculosAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculoController : ControllerBase
{
    private VeiculoContext _context;
    private IMapper _mapper;

    public VeiculoController(VeiculoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaVeiculo([FromBody] CreateVeiculoDto veiculoDto) //From body vendo do corpo da requisição // Padrão REST aplicado
    {
        // outra possibilidade, além do automapper
        /*Veiculo veiculo = new Veiculo 
        {
            Marca = veiculoDto.Marca,
            Modelo = veiculoDto.Modelo,
            Ano = veiculoDto.Ano
        };*/


        Veiculo veiculo = _mapper.Map<Veiculo>(veiculoDto);
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

    [HttpPut("{id}")]
    public IActionResult AtualizaVeiculo(int id,
        [FromBody] UpdateVeiculoDto veiculoDto )
    {
        var veiculo = _context.Veiculos.FirstOrDefault(
            veiculo => veiculo.Id == id);
        
        if (veiculo == null) return NotFound();

        _mapper.Map(veiculoDto, veiculo);
        _context.SaveChanges();
        return NoContent(); // Status cod para atualização Padrão REST - Cod 204 
    }

    [HttpPatch("{id}")] // Mudanças parciais em JSON no código é necessario um Lib NewtonSoft
    public IActionResult AtualizaVeiculoParcial(int id,
        JsonPatchDocument<UpdateVeiculoDto> patch)
    {
        var veiculo = _context.Veiculos.FirstOrDefault(
            veiculo => veiculo.Id == id);

        if (veiculo == null) return NotFound();

        var veiculoParaAtualizar = _mapper.Map<UpdateVeiculoDto>(veiculo);

        patch.ApplyTo(veiculoParaAtualizar, ModelState);

        if (!TryValidateModel(veiculoParaAtualizar)) return ValidationProblem(ModelState);

        _mapper.Map(veiculoParaAtualizar, veiculo);
        _context.SaveChanges();
        return NoContent(); // Status cod para atualização Padrão REST - Cod 204 
    }
}
