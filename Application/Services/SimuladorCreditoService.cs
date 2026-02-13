using System.Text;
using BtgSimuladorCredito.Domain;
using BtgSimuladorCredito.Application.DTOs;
using BtgSimuladorCredito.Infrastructure.Data;
using BtgSimuladorCredito.Infrastructure.Entities;


namespace BtgSimuladorCredito.Application.Services;
public class SimuladorCreditoService
{
    private readonly ApplicationDbContext _context;

    public SimuladorCreditoService(ApplicationDbContext context)
    {
        _context = context;
    }


    public List<Parcela> Simular(SimuladorRequisicao request)
{
    var Parcelas = new List<Parcela>();

    if (!Enum.IsDefined(typeof(FrequenciaPagamento), request.Frequencia))
        throw new ArgumentException("Frequência inválida.");

    if (request.DataFim <= request.DataInicio)
        throw new ArgumentException("Data final deve ser maior que a data inicial.");

    if (request.Principal <= 0)
        throw new ArgumentException("Principal deve ser maior que zero.");

    if (request.TaxaJurosAnual <= 0)
        throw new ArgumentException("Taxa de juros deve ser maior que zero.");

    int periodosPorAno = GetPeriodosPorAno(request.Frequencia);
    decimal TaxaPeriodo = ConverteTaxaAnual(request.TaxaJurosAnual, periodosPorAno);

    int totalPeriodo = CalculoTotalPeriodo(request.DataInicio, request.DataFim, request.Frequencia);

    decimal principalBase = Math.Round(request.Principal / totalPeriodo, 2);
    decimal somaPrincipal = 0;

    DateTime dataPagamento = request.DataInicio;

    for (int i = 1; i <= totalPeriodo; i++)
    {
        dataPagamento = AdicionarPeriodo(dataPagamento, request.Frequencia);
        dataPagamento = AdicionarProxDiaUtil(dataPagamento);

        decimal valorPrincipalAtual = principalBase;

        if (i == totalPeriodo)
        {
            valorPrincipalAtual = request.Principal - somaPrincipal;
        }

        somaPrincipal += valorPrincipalAtual;

        decimal valorFuturo = valorPrincipalAtual *
            (decimal)Math.Pow((double)(1 + TaxaPeriodo), i);

        Parcelas.Add(new Parcela
        {
            DataPagamento = dataPagamento,
            ValorPrincipal = valorPrincipalAtual,
            TotalComJuros = Math.Round(valorFuturo, 2, MidpointRounding.AwayFromZero)
        });
    }

    var simulacao = new Simulacao
    {
        Principal = request.Principal,
        DataInicio = request.DataInicio,
        DataFim = request.DataFim,
        TaxaJurosAnual = request.TaxaJurosAnual,
        Frequencia = (int)request.Frequencia
    };

    foreach (var parcela in Parcelas)
    {
        simulacao.Parcelas.Add(new ParcelaEntity
        {
            DataPagamento = parcela.DataPagamento,
            ValorPrincipal = parcela.ValorPrincipal,
            TotalComJuros = parcela.TotalComJuros
        });
    }

    _context.Simulacoes.Add(simulacao);
    _context.SaveChanges();

    return Parcelas;
}


    private int GetPeriodosPorAno (FrequenciaPagamento frequencia)
    {
        return frequencia switch
        {
            FrequenciaPagamento.Mensal => 12,
            FrequenciaPagamento.Trimestral => 4,
            FrequenciaPagamento.Semestral => 2,
            FrequenciaPagamento.Anual => 1,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private decimal ConverteTaxaAnual(decimal taxaAnual, int periodosPorAno)
    {
        return (decimal)(Math.Pow((double)(1 + taxaAnual), 1.0 / periodosPorAno) - 1);
    }

    private int CalculoTotalPeriodo (DateTime inicio, DateTime fim, FrequenciaPagamento frequencia)
    {
        int meses = ((fim.Year - inicio.Year)* 12) + fim.Month - inicio.Month;

        return frequencia switch
        {
            FrequenciaPagamento.Mensal => meses,
            FrequenciaPagamento.Trimestral => meses / 3,
            FrequenciaPagamento.Semestral => meses / 6,
            FrequenciaPagamento.Anual => meses / 12,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private DateTime AdicionarPeriodo (DateTime data, FrequenciaPagamento frequencia)
    {
        return frequencia switch
        {
            FrequenciaPagamento.Mensal => data.AddMonths(1),
            FrequenciaPagamento.Trimestral => data.AddMonths(3),
            FrequenciaPagamento.Semestral => data.AddMonths(6),
            FrequenciaPagamento.Anual => data.AddMonths(12),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private DateTime AdicionarProxDiaUtil (DateTime data)
    {
        if (data.DayOfWeek == DayOfWeek.Saturday)
            return data.AddDays(2);
        
        if (data.DayOfWeek == DayOfWeek.Sunday)
            return data.AddDays(1);

        return data;
    }

}

