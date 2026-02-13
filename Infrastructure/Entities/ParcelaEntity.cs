namespace BtgSimuladorCredito.Infrastructure.Entities;

public class ParcelaEntity
{
    public Guid id {get; set;} = Guid.NewGuid();
    public DateTime DataPagamento {get; set;}
    public decimal ValorPrincipal {get; set;}
    public decimal TotalComJuros {get; set;}

    public Guid SimulacaoId {get; set;}
}