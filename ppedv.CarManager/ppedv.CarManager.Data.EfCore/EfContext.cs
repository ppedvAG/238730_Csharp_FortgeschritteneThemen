using Microsoft.EntityFrameworkCore;
using ppedv.CarManager.Model;

namespace ppedv.CarManager.Data.EfCore
{
    public class EfContext : DbContext
    {
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarsManager;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
