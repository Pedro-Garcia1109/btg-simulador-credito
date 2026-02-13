using Microsoft.EntityFrameworkCore;
using BtgSimuladorCredito.Infrastructure.Entities;

namespace BtgSimuladorCredito.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {  
    }

    public DbSet<Simulacao> Simulacoes {get; set;}
    public DbSet<ParcelaEntity> Parcelas {get; set;}
}