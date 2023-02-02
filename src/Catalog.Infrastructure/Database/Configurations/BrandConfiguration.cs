using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.HasKey(x => x.BrandId);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Brand)
            .HasForeignKey(x => x.BrandId);


        builder.HasData(
            new BrandEntity
            {
                BrandId = SeedDataConstants.BrandApple,
                Name = "Apple",
                Image = "https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg"
            },
            new BrandEntity
            {
                BrandId = SeedDataConstants.BrandSamsung,
                Name = "Samsung",
                Image = "https://upload.wikimedia.org/wikipedia/commons/2/24/Samsung_Logo.svg"
            },
            new BrandEntity
            {
                BrandId = SeedDataConstants.BrandLg,
                Name = "Lg",
                Image = "https://upload.wikimedia.org/wikipedia/commons/2/24/Samsung_Logo.svg"
            }
        );
    }
}