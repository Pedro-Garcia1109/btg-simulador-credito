namespace BtgSimuladorCredito.Infrastructure.Entities;

public class Simulacao
{
    public Guid Id {get; set;} = Guid.NewGuid();

    public decimal Principal {get; set;}
    public DateTime DataInicio {get; set;}
    public DateTime DataFim {get; set;}
    public decimal TaxaJurosAnual {get; set;}
    public int Frequencia {get; set;}

    public List <ParcelaEntity> Parcelas {get; set;} = new();
}