using Microsoft.EntityFrameworkCore;
using WebApiConfigurations.DAL.Configurations;
using WebApiConfigurations.Entities;

namespace WebApiConfigurations.DAL.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof (ProductConfiguration).Assembly);
        }
    }
}
