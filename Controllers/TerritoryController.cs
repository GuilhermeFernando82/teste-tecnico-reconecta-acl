using Apivscode2.Interfaces;
using Apivscode2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apivscode2.Controllers;

[ApiController]
[Route("[controller]")]
public class TerritoryController : ControllerBase
{
    private readonly ITerritoryRepository _repository;
    private readonly IProducerRepository _producerRepository;

    public TerritoryController(
        ITerritoryRepository repository,
        IProducerRepository producerRepository)
    {
        _repository = repository;
        _producerRepository = producerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var territories = await _repository.SearchTerritoryAsync();
        return territories.Any() ? Ok(territories) : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Território inválido.");

        var territory = await _repository.SearchTerritoryByIdAsync(id);
        return territory != null ? Ok(territory) : NotFound("Território não encontrado.");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TerritoryRequestDTO request)
    {
        if (request.ProducerId <= 0)
            return BadRequest("Produtor é obrigatório.");

        var producer = await _producerRepository.SearcProducerByIdAsync(request.ProducerId);
        if (producer == null)
            return BadRequest("Produtor não existe.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Nome é obrigatório.");

        if (request.Area <= 0)
            return BadRequest("Área deve ser maior que zero.");

        if (request.Type != "Rural" && request.Type != "Urbano")
            return BadRequest("Tipo deve ser Rural ou Urbano.");

        var added = await _repository.AddTerritoryAsync(request);

        return added
            ? Ok("Território cadastrado com sucesso!")
            : BadRequest("Erro ao cadastrar território.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] TerritoryRequestDTO request, int id)
    {
        if (id <= 0)
            return BadRequest("Território inválido.");

        var territory = await _repository.SearchTerritoryByIdAsync(id);
        if (territory == null)
            return NotFound("Território não existe.");

        var producer = await _producerRepository.SearcProducerByIdAsync(request.ProducerId);
        if (producer == null)
            return BadRequest("Produtor não existe.");

        var updated = await _repository.UpdateTerritoryAsync(request, id);

        return updated
            ? Ok("Território atualizado com sucesso!")
            : BadRequest("Erro ao atualizar território.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Território inválido.");

        var deleted = await _repository.DeleteTerritoryAsync(id);

        return deleted
            ? Ok("Território deletado com sucesso!")
            : BadRequest("Erro ao deletar território.");
    }
}
