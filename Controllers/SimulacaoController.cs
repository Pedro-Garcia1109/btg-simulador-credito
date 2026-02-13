using Microsoft.AspNetCore.Mvc;
using BtgSimuladorCredito.Application.Services;
using BtgSimuladorCredito.Application.DTOs;
using BtgSimuladorCredito.Infrastructure.Data;

namespace BtgSimuladorCredito.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulacaoController : ControllerBase
{
    private readonly SimuladorCreditoService _service;

    public SimulacaoController (SimuladorCreditoService service)
    {
        _service = service;
    }

    [HttpGet("ListaSimulacoes")]

    public IActionResult ListarHistorico ([FromServices] ApplicationDbContext context)
    {
        var simulacoes = context.Simulacoes
            .Select(s => new
            {
                s.Id,
                s.Principal,
                s.DataInicio,
                s.DataFim,
                s.TaxaJurosAnual,
                s.Frequencia,
                Parcela = s.Parcelas
            })
            .ToList();

        return Ok(simulacoes);    
    }

    [HttpPost]
    public IActionResult Simular ([FromBody] SimuladorRequisicao requisicao)
    {
        var result = _service.Simular(requisicao);
        return Ok (result);
    }
}