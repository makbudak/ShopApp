using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp.Model.Entity;

namespace ShopApp.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(m=>m.Id);

            builder.Property(m=>m.Name).IsRequired().HasMaxLength(100);
            
        }
    }
}