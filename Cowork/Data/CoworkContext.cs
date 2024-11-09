using Cowork.Models;
using Microsoft.EntityFrameworkCore;

namespace Cowork.Data
{
    public class CoworkContext : DbContext
    {
        public CoworkContext(DbContextOptions<CoworkContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           // Configuração para relacionamento N:M entre Reserva e Funcionario
            modelBuilder.Entity<Reserva>()
                .HasMany(r => r.Funcionarios)
                .WithMany(f => f.Reservas)
                .UsingEntity(j => j.ToTable("ReservaFuncionarios"));

            // Configuração para relacionamento 1:N entre Cliente e Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId);

            // Configuração para relacionamento 1:N entre Sala e Reserva
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Sala)
                .WithMany(s => s.Reservas)
                .HasForeignKey(r => r.SalaId);

            // Configurações adicionais podem ser adicionadas conforme necessário
        }
    }
}
