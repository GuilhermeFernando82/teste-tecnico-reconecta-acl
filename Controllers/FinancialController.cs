using Apivscode2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apivscode2.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancialController : ControllerBase
{
    private readonly IFinancialService _service;

    public FinancialController(IFinancialService service)
    {
        _service = service;
    }

    [HttpGet("situation/{document}")]
    public async Task<IActionResult> GetFinancialSituation(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            return BadRequest("Documento é obrigatório.");

        var result = await _service.GetFinancialSituationAsync(document);

        return Ok(result);
    }
}
