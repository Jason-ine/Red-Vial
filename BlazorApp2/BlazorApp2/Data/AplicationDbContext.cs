using BlazorApp2.Share;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlazorApp2.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<DetalleSemaforo> DetalleSemaforo { get; set; }
        public DbSet<DetalleSemaforo> EstadisticaSemaforo { get; set; }
    }
}
