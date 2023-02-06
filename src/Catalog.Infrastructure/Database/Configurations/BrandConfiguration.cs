using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Brand)
            .HasForeignKey(x => x.BrandId);


        builder.HasData(
            new BrandEntity
            {
                Id = SeedDataConstants.BrandApple,
                Name = "Apple",
                Image = "https://blob.com/BrandApple.png"
            },
            new BrandEntity
            {
                Id = SeedDataConstants.BrandSamsung,
                Name = "Samsung",
                Image = "https://blob.com/BrandSamsung.png"
            },
            new BrandEntity
            {
                Id = SeedDataConstants.BrandLg,
                Name = "Lg",
                Image = "https://blob.com/BrandLg.png"
            },
            new BrandEntity
            {
                Id = SeedDataConstants.BrandLenovo,
                Name = "Lenovo",
                Image = "https://blob.com/BrandLenovo.png"
            }
        );
    }
}