using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Images)
            .WithOne(x => x.ProductEntity)
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.FullPrice)
            .HasColumnType("decimal(18, 2)");

        builder.Property(x => x.Discount)
            .HasColumnType("decimal(18, 2)");


        builder.HasData(
            new ProductEntity
            {
                Id = SeedDataConstants.Phone1,
                Name = "Apple iPhone 12 Pro Max",
                Description = "The Apple iPhone 12 Pro Max features a 6.7-inch Super Retina XDR display," +
                              " A14 Bionic chip, and a triple-camera system.",
                FullPrice = 1099.99M,
                Discount = 0.0M,
                Quantity = 15,
                IsActive = true,
                BrandId = SeedDataConstants.BrandApple,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Phone2,
                Name = "Apple iPhone 12 Mini",
                Description = "The Apple iPhone 12 Mini features a 5.4-inch Super Retina XDR display," +
                              " A14 Bionic chip, and a dual-camera system.",
                FullPrice = 699.99M,
                Discount = 0.0M,
                Quantity = 20,
                IsActive = true,
                BrandId = SeedDataConstants.BrandApple,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Phone3,
                Name = "Samsung Galaxy S21",
                Description = "The Samsung Galaxy S21 features a 6.2-inch AMOLED display," +
                              " Exynos 2100 or Snapdragon 888 processor, and a triple camera system.",
                FullPrice = 899.99M,
                Discount = 0.0M,
                Quantity = 20,
                IsActive = true,
                BrandId = SeedDataConstants.BrandSamsung,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Phone4,
                Name = "Samsung Galaxy Note 20",
                Description = "The Samsung Galaxy Note 20 features a 6.7-inch AMOLED display," +
                              " Snapdragon 865+ processor, and a triple camera system.",
                FullPrice = 799.99M,
                Discount = 49.990M,
                Quantity = 15,
                IsActive = true,
                BrandId = SeedDataConstants.BrandSamsung,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Phone5,
                Name = "Samsung Galaxy Z Flip",
                Description = "The Samsung Galaxy Z Flip features a 6.7-inch foldable AMOLED display," +
                              " Snapdragon 855+ processor, and a dual camera system.",
                FullPrice = 999.99M,
                Discount = 100.0M,
                Quantity = 0,
                IsActive = false,
                BrandId = SeedDataConstants.BrandSamsung,
                CategoryId = SeedDataConstants.CategoryPhone
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Tv1,
                Name = "Samsung QN55Q70T 55-Inch 4K UHD Smart TV",
                Description = "The Samsung QN55Q70T features a 55-inch 4K UHD display," +
                              " Quantum Processor 4K, and Real Game Enhancer technology.",
                FullPrice = 799.99M,
                Discount = 99.99M,
                Quantity = 10,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandSamsung
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Tv2,
                Name = "Samsung QN82Q70T 82-Inch 4K UHD Smart TV",
                Description = "The Samsung QN82Q70T features an 82-inch 4K UHD display, Quantum Processor 4K," +
                              " and Real Game Enhancer technology.",
                FullPrice = 2199.99M,
                Discount = 0.0M,
                Quantity = 5,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandSamsung
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Tv3,
                Name = "Samsung QN85Q900RB 85-Inch 8K UHD Smart TV",
                Description = "The Samsung QN85Q900RB features an 85-inch 8K UHD display, Quantum Processor 8K," +
                              " and Real Game Enhancer technology.",
                FullPrice = 3999.99M,
                Discount = 399.99M,
                Quantity = 13,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandSamsung
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Tv4,
                Name = "LG OLED65CXP 65-Inch 4K UHD Smart TV",
                Description = "The LG OLED65CXP features a 65-inch 4K UHD OLED display," +
                              " α9 Gen 3 AI Processor 4K, and Dolby Vision IQ technology.",
                FullPrice = 1999.99M,
                Discount = 0.0M,
                Quantity = 0,
                IsActive = false,
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandLg
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Tv5,
                Name = "LG OLED77GX 77-Inch 4K UHD Smart TV",
                Description = "The LG OLED77GX features a 77-inch 4K UHD OLED display," +
                              " α9 Gen 3 AI Processor 4K, and Dolby Vision IQ technology.",
                FullPrice = 4999.99M,
                Discount = 999.99M,
                Quantity = 1,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandLg
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook1,
                Name = "Lenovo ThinkPad X1 Carbon 14-Inch Laptop",
                Description = "The Lenovo ThinkPad X1 Carbon features a 14-inch full HD display," +
                              " 8th Gen Intel Core i7 processor, and 16GB RAM.",
                FullPrice = 999.99M,
                Discount = 0.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook2,
                Name = "Lenovo Ideapad 330 15-Inch Laptop",
                Description = "The Lenovo Ideapad 330 features a 15-inch full HD display," +
                              " Intel Celeron N4000 processor, and 4GB RAM.",
                FullPrice = 499.99M,
                Discount = 199.99M,
                Quantity = 20,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook3,
                Name = "Lenovo Legion Y540 15-Inch Gaming Laptop",
                Description = "The Lenovo Legion Y540 features a 15-inch full HD display," +
                              " 9th Gen Intel Core i7 processor, and 16GB RAM.",
                FullPrice = 1299.99M,
                Discount = 0.0M,
                Quantity = 15,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook4,
                Name = "Lenovo Yoga C930 2-In-1 Laptop",
                Description = "The Lenovo Yoga C930 features a 13.9-inch full HD display," +
                              " 8th Gen Intel Core i7 processor, and 8GB RAM.",
                FullPrice = 1299.99M,
                Discount = 100.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook5,
                Name = "Lenovo ThinkPad X1 Yoga",
                Description = "The Lenovo ThinkPad X1 Yoga features a 14-inch Full HD IPS touch screen," +
                              " Intel Core i7 processor, and 8GB of RAM.",
                FullPrice = 1599.99M,
                Discount = 0.0M,
                Quantity = 20,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook6,
                Name = "Lenovo Legion 5",
                Description = "The Lenovo Legion 5 features a 15.6-inch Full HD IPS display," +
                              " AMD Ryzen 5 4600H processor, and NVIDIA GeForce GTX 1650 graphics card.",
                FullPrice = 999.99M,
                Discount = 0.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook7,
                Name = "Lenovo IdeaPad 5",
                Description = "The Lenovo IdeaPad 5 features a 14-inch Full HD IPS display," +
                              " Intel Core i5 processor, and 8GB of RAM.",
                FullPrice = 699.99M,
                Discount = 100.0M,
                Quantity = 15,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook8,
                Name = "Lenovo Yoga 9i",
                Description = "The Lenovo Yoga 9i features a 14-inch Full HD IPS touch screen," +
                              " Intel Core i7 processor, and 16GB of RAM.",
                FullPrice = 1799.99M,
                Discount = 200.0M,
                Quantity = 0,
                IsActive = false,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook9,
                Name = "Lenovo ThinkPad P1",
                Description = "The Lenovo ThinkPad P1 features a 15.6-inch Full HD IPS display," +
                              " Intel Core i7 processor, and NVIDIA Quadro P620 graphics card.",
                FullPrice = 2199.99M,
                Discount = 300.0M,
                Quantity = 3,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandLenovo
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook10,
                Name = "Apple MacBook Pro 16-inch",
                Description = "The Apple MacBook Pro 16-inch features a 16-inch Retina Display," +
                              " 9th-generation 6-Core Intel Core i7 processor, and AMD Radeon Pro 5300M graphics card.",
                FullPrice = 2499.99M,
                Discount = 500.0M,
                Quantity = 0,
                IsActive = false,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook11,
                Name = "Apple MacBook Air 13-inch",
                Description = "The Apple MacBook Air 13-inch features a 13.3-inch Retina Display," +
                              " 8th-generation dual-core Intel Core i5 processor, and 8GB of RAM.",
                FullPrice = 999.99M,
                Discount = 100.0M,
                Quantity = 10,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook12,
                Name = "Apple MacBook Pro 15-inch",
                Description = "The Apple MacBook Pro 15-inch features a 15.4-inch Retina Display," +
                              " 9th-generation 8-Core Intel Core i9 processor, and AMD Radeon Pro 555X graphics card.",
                FullPrice = 2799.99M,
                Discount = 700.0M,
                Quantity = 5,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook13,
                Name = "Apple MacBook 12-inch",
                Description = "The Apple MacBook 12-inch features a 12-inch Retina Display," +
                              " 7th-generation dual-core Intel Core m3 processor, and 8GB of RAM.",
                FullPrice = 799.99M,
                Discount = 50.0M,
                Quantity = 20,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook14,
                Name = "Apple MacBook Pro 13-inch",
                Description = "The Apple MacBook Pro 13-inch features a 13.3-inch Retina Display," +
                              " 10th-generation quad-core Intel Core i5 processor, and Intel Iris Plus Graphics.",
                FullPrice = 1499.99M,
                Discount = 250.0M,
                Quantity = 8,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook15,
                Name = "Apple MacBook Pro 16-inch",
                Description = "The Apple MacBook Pro 16-inch features a 16-inch Retina display," +
                              " 9th generation Intel Core i9 processor, and 32GB of RAM.",
                FullPrice = 2499.99M,
                Discount = 500.0M,
                Quantity = 2,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook16,
                Name = "Apple MacBook 12-inch",
                Description = "The Apple MacBook 12-inch features a 12-inch Retina display," +
                              " Intel Core m3 processor, and 8GB of RAM.",
                FullPrice = 799.99M,
                Discount = 100.0M,
                Quantity = 6,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook17,
                Name = "Apple MacBook Pro Touch Bar",
                Description = "The Apple MacBook Pro Touch Bar features a 13.3-inch Retina display," +
                              " Intel Core i5 processor, and 16GB of RAM.",
                FullPrice = 1299.99M,
                Discount = 150.0M,
                Quantity = 4,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            },
            new ProductEntity
            {
                Id = SeedDataConstants.Notebook18,
                Name = "Apple MacBook Pro Touch Bar 15-inch",
                Description = "The Apple MacBook Pro Touch Bar 15-inch features a 15-inch Retina display," +
                              " Intel Core i7 processor, and 32GB of RAM.",
                FullPrice = 1799.99M,
                Discount = 250.0M,
                Quantity = 3,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryNotebook,
                BrandId = SeedDataConstants.BrandApple
            }
        );
    }
}