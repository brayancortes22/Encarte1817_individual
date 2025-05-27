using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Entity.Context
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        public DbSet<ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración específica para la tabla de logs
            modelBuilder.Entity<ChangeLog>(entity =>
            {
                entity.ToTable("ChangeLogs");
                entity.HasKey(e => e.Id);
                // Puedes agregar más configuraciones si son necesarias
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
