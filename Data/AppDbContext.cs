using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PersonRecord> TbBaseDotCcrInEx => Set<PersonRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRecord>().HasKey(p => p.RutDni);
        }
    }
}
