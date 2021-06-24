using Microsoft.EntityFrameworkCore;
using ShopApp.Extensions;
using ShopApp.Model.Entity;
using ShopApp.Model.Enum;
using System;

namespace ShopApp.Data.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Samsung S5", Url = "samsung-s5", Price = 2000, Description = "iyi telefon", IsApproved = true },
                new Product() { Id = 2, Name = "Samsung S6", Url = "samsung-s6", Price = 3000, Description = "iyi telefon", IsApproved = false },
                new Product() { Id = 3, Name = "Samsung S7", Url = "samsung-s7", Price = 4000, Description = "iyi telefon", IsApproved = true },
                new Product() { Id = 4, Name = "Samsung S8", Url = "samsung-s8", Price = 5000, Description = "iyi telefon", IsApproved = false },
                new Product() { Id = 5, Name = "Samsung S9", Url = "samsung-s9", Price = 6000, Description = "iyi telefon", IsApproved = true }
            );

            builder.Entity<ProductImage>().HasData(
                new ProductImage() { Id = 1, ImageUrl = "1.jpg", ProductId = 1, Order = 1 },
                new ProductImage() { Id = 2, ImageUrl = "2.jpg", ProductId = 2, Order = 1 },
                new ProductImage() { Id = 3, ImageUrl = "3.jpg", ProductId = 3, Order = 1 },
                new ProductImage() { Id = 4, ImageUrl = "4.jpg", ProductId = 4, Order = 1 },
                new ProductImage() { Id = 5, ImageUrl = "5.jpg", ProductId = 5, Order = 1 }
                );

            builder.Entity<Category>().HasData(
                            new Category() { Id = 1, Name = "Telefon", Url = "telefon" },
                            new Category() { Id = 2, Name = "Bilgisayar", Url = "bilgisayar" },
                            new Category() { Id = 3, Name = "Elektronik", Url = "elektronik" },
                            new Category() { Id = 4, Name = "Beyaz Eşya", Url = "beyaz-esya" }
                        );

            builder.Entity<ProductCategory>().HasData(
                new ProductCategory() { CategoryId = 1, ProductId = 1, Id = 1 },
                new ProductCategory() { CategoryId = 2, ProductId = 1, Id = 2 },
                new ProductCategory() { CategoryId = 3, ProductId = 1, Id = 3 },
                new ProductCategory() { CategoryId = 1, ProductId = 2, Id = 4 },
                new ProductCategory() { CategoryId = 2, ProductId = 2, Id = 5 },
                new ProductCategory() { CategoryId = 3, ProductId = 2, Id = 6 },
                new ProductCategory() { CategoryId = 1, ProductId = 3, Id = 7 },
                new ProductCategory() { CategoryId = 1, ProductId = 4, Id = 8 },
                new ProductCategory() { CategoryId = 1, ProductId = 5, Id = 9 },
                new ProductCategory() { CategoryId = 2, ProductId = 5, Id = 10 }
                );

            builder.Entity<User>().HasData(
                new User() { Deleted = false, Email = "akbudak.mehmet@gmail.com", EmailConfirmed = true, Id = 1, InsertedDate = DateTime.Now, IsActive = true, Name = "Mehmet", Surname = "Akbudak", UserType = UserType.SuperAdmin, Password = HashExtension.Sha256("1") }
                );

        }
    }
}