using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BtgSimuladorCredito.Application.Services;
using BtgSimuladorCredito.Application.DTOs;
using BtgSimuladorCredito.Domain;

namespace BtgSimuladorCredito.Pages;

[IgnoreAntiforgeryToken]
public class IndexModel : PageModel
{
    private readonly SimuladorCreditoService _service;

    public IndexModel(SimuladorCreditoService service)
    {
        _service = service;
    }

    [BindProperty]
    public decimal Principal { get; set; }

    [BindProperty]
    public DateTime DataInicio { get; set; }

    [BindProperty]
    public DateTime DataFim { get; set; }

    [BindProperty]
    public decimal? TaxaJurosAnual { get; set; }

    [BindProperty]
    public int Frequencia { get; set; }

    public List<Parcela>? Parcelas { get; set; }

    public void OnGet()
    {
        DataInicio = DateTime.Today;
        DataFim = DateTime.Today.AddMonths(12);
    }

    public IActionResult OnPost()
    {

    if (!ModelState.IsValid)
    {
        return Page();
    }

    if (!TaxaJurosAnual.HasValue)
    {
        ModelState.AddModelError("", "Informe a taxa anual.");
        return Page();
    }

    try
    {
        
        var request = new SimuladorRequisicao
        {
            Principal = Principal,
            DataInicio = DataInicio,
            DataFim = DataFim,
            TaxaJurosAnual = TaxaJurosAnual.Value,
            Frequencia = (FrequenciaPagamento)Frequencia
        };

        Parcelas = _service.Simular(request);
    }
    catch (Exception ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
    }
        return Page();
    }   
}
