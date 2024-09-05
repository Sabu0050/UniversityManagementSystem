using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityManagementSystem.DLL.Model;

namespace UniversityManagementSystem.DLL.Configs
{
    public class CategoryProductConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder
                .HasKey(cp=>new { cp.CategoryId,cp.ProductId});

            builder
                .HasOne(cp => cp.Product)
                .WithMany(cp => cp.CategoryProducts)
                .HasForeignKey(cp => cp.ProductId);
            builder
                .HasOne(cp=> cp.Category)
                .WithMany(cp=> cp.CategoryProducts)
                .HasForeignKey(cp => cp.CategoryId);
        }
    }
}
