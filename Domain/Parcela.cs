namespace BtgSimuladorCredito.Domain;

public class Parcela
{
    public DateTime DataPagamento {get; set;}
    public decimal ValorPrincipal {get; set;}
    public decimal TotalComJuros {get; set;}
}