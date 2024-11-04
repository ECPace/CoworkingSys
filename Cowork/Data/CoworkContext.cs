using Cowork.Models;
using Microsoft.EntityFrameworkCore;

namespace Cowork.Data
{
    public class CoworkContext : DbContext
    {
        public CoworkContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        public DbSet<VeiculoLocado> VeiculosLocados { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VeiculoLocado>()
                .HasKey(vl => new { vl.LocacaoId, vl.VeiculoId });
        }
    }
}
