using BtgSimuladorCredito.Domain;

namespace BtgSimuladorCredito.Application.DTOs;

public class SimuladorRequisicao
{
    public decimal Principal {get; set;}
    public DateTime DataInicio {get; set;}
    public DateTime DataFim {get; set;}
    public decimal TaxaJurosAnual {get; set;}
    public FrequenciaPagamento Frequencia {get; set;}
}