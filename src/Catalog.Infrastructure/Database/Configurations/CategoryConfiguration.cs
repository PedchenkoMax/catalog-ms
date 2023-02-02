using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.CategoryId);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);


        builder.HasData(
            new CategoryEntity
            {
                CategoryId = SeedDataConstants.CategoryPhone,
                Name = "Phone",
                Image = "https://cdn-icons-png.flaticon.com/512/65/65680.png"
            },
            new CategoryEntity
            {
                CategoryId = SeedDataConstants.CategoryTv,
                Name = "TV",
                Image = "https://cdn-icons-png.flaticon.com/512/3443/3443580.png"
            }
        );
    }
}