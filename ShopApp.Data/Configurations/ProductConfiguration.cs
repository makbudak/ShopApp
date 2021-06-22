using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopApp.Model.Entity;

namespace ShopApp.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(m=>m.Id);

            builder.Property(m=>m.Name).IsRequired().HasMaxLength(100);

            builder.Property(m=>m.DateAdded).HasDefaultValueSql("getdate()");  // mssql
            // builder.Property(m=>m.DateAdded).HasDefaultValueSql ("date('now')");   // sqlite            
        }
    }
}