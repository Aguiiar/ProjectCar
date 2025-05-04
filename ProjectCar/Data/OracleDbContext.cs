using System.Data.Entity;
using System.Reflection;
using System.Reflection.Emit;
using ProjectCar.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCar.Repositories
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext() : base("name=OracleDbContext")
        {
            Database.SetInitializer<OracleDbContext>(null);
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SYS");
            modelBuilder.Entity<Brand>().ToTable("BRANDS");
            modelBuilder.Entity<Car>().ToTable("CARS");

            modelBuilder.Entity<Car>()
                .HasRequired(c => c.Brand)
                .WithMany()
                .HasForeignKey(c => c.BrandId);

            base.OnModelCreating(modelBuilder);
        }
    }
}