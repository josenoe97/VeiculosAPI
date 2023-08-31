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

    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="veiculoDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Retorna todos os filmes do bando de dados
    /// </summary>
    /// <param name="veiculoDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadVeiculoDto> RecuperaVeiculos([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadVeiculoDto>>(_context.Veiculos.Skip(skip).Take(take)); // Escolha a faixa de veiculos a ser selecionada!!
    }

    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Retorna o filme de acordo com Id
    /// </summary>
    /// <param name="veiculoDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult RecuperaVeiculoPorId(int id) // IActionResult é padrão REST
    {
        var veiculo = _context.Veiculos.FirstOrDefault(veiculo => veiculo.Id == id);
        if (veiculo == null) return NotFound();

        var veiculoDto = _mapper.Map<ReadVeiculoDto>(veiculo);


        return Ok(veiculoDto);
    }

    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Altera os dados de um filme existente
    /// </summary>
    /// <param name="veiculoDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
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

    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Altera os dados pacialmente de um filme existente
    /// </summary>
    /// <param name="veiculoDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
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

    //----------------------------------------------------------------------------------------
    /// <summary>
    /// Deleta um dado existente
    /// </summary>
    /// <param name="veiculoDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeletaVeiculo(int id)
    {
        var veiculo = _context.Veiculos.FirstOrDefault(
            veiculo => veiculo.Id == id);

        if(veiculo == null) return NotFound();

        _context.Remove(veiculo);
        _context.SaveChanges();
        return NoContent();
    }
}
