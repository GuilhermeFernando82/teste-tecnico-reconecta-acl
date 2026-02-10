using Apivscode2.Interfaces;
using Apivscode2.Models;
using Apivscode2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apivscode2.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducerController : ControllerBase
{
    private readonly IProducerRepository _repository;
    private readonly IProducerService _service;
    private readonly IFinancialService _financialService;
    public ProducerController(
        IProducerRepository repository,
        IProducerService service,
        IFinancialService financialService)
    {
        _repository = repository;
        _service = service;
        _financialService = financialService;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var producers = await _repository.SearchProducerAsync();
        return producers.Any() ? Ok(producers) : NoContent();
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Produtor inválido.");

        var producer = await _repository.SearcProducerByIdAsync(id);
        return producer != null ? Ok(producer) : NotFound("Produtor não encontrado.");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProducerRequestDTO request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Document))
            return BadRequest("Documento é obrigatório.");

        var added = await _repository.AddProducerAsync(request);

        return added
            ? Ok("Produtor cadastrado com sucesso!")
            : BadRequest("Erro ao cadastrar produtor.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] ProducerRequestDTO request, int id)
    {
        if (id <= 0)
            return BadRequest("Produtor inválido.");

        var producer = await _repository.SearcProducerByIdAsync(id);
        if (producer == null)
            return NotFound("Produtor não existe na base de dados.");

        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Document))
            return BadRequest("Documento é obrigatório.");

        var updated = await _repository.UpdateProducer(request, id);

        return updated
            ? Ok("Produtor atualizado com sucesso!")
            : BadRequest("Erro ao atualizar produtor.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Produtor inválido.");

        var deleted = await _repository.DeleteCustomerAsync(id);

        return deleted
            ? Ok("Produtor deletado com sucesso!")
            : BadRequest("Erro ao deletar produtor.");
    }
    [HttpPatch("activate/{id}")]
    public async Task<IActionResult> Activate(int id)
    {
        try
        {
            var activated = await _service.ActivateProducerAsync(id);

            return activated
                ? Ok("Produtor ativado com sucesso!")
                : BadRequest("Erro ao ativar produtor.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
