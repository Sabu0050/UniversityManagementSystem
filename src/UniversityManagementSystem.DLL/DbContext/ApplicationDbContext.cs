using Azure;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.DLL.Configs;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.DLL.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            // modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        
    }
}
