using Microsoft.EntityFrameworkCore;

namespace CarInsurance.Models
{
    public class CarInsuranceContext : DbContext
    {
        public CarInsuranceContext(DbContextOptions<CarInsuranceContext> options)
            : base(options)
        {
        }

        public DbSet<Insuree> Insurees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ SQLite compatible type
            modelBuilder.Entity<Insuree>()
                .Property(i => i.Quote)
                .HasColumnType("REAL");
        }
    }
}
