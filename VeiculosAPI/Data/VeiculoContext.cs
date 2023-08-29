using Microsoft.EntityFrameworkCore;
using VeiculosAPI.Models;

namespace VeiculosAPI.Data;

public class VeiculoContext : DbContext
{
    public VeiculoContext(DbContextOptions<VeiculoContext> opts) 
        : base(opts)
    {
        
    }

    public DbSet<Veiculo> Veiculos { get; set; }
}
