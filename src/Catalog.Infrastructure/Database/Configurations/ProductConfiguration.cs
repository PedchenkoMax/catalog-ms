using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(x => x.ProductId);

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
                ProductId = SeedDataConstants.Phone1,
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
                ProductId = SeedDataConstants.Phone2,
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
                ProductId = SeedDataConstants.Phone3,
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
                ProductId = SeedDataConstants.Phone4,
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
                ProductId = SeedDataConstants.Phone5,
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
                ProductId = SeedDataConstants.Tv1,
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
                ProductId = SeedDataConstants.Tv2,
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
                ProductId = SeedDataConstants.Tv3,
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
                ProductId = SeedDataConstants.Tv4,
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
                ProductId = SeedDataConstants.Tv5,
                Name = "LG OLED77GX 77-Inch 4K UHD Smart TV",
                Description = "The LG OLED77GX features a 77-inch 4K UHD OLED display," +
                              " α9 Gen 3 AI Processor 4K, and Dolby Vision IQ technology.",
                FullPrice = 4999.99M,
                Discount = 999.99M,
                Quantity = 1,
                IsActive = true,
                CategoryId = SeedDataConstants.CategoryTv,
                BrandId = SeedDataConstants.BrandLg
            });
    }
}