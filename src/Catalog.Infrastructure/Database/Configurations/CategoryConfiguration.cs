using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId);


        builder.HasData(
            new CategoryEntity
            {
                Id = SeedDataConstants.CategoryPhone,
                Name = "Phone",
                Image = "https://blob.com/CategoryPhone.png"
            },
            new CategoryEntity
            {
                Id = SeedDataConstants.CategoryTv,
                Name = "TV",
                Image = "https://blob.com/CategoryTv.png"
            },
            new CategoryEntity
            {
                Id = SeedDataConstants.CategoryNotebook,
                Name = "Notebook",
                Image = "https://blob.com/CategoryNotebook.png"
            }
        );
    }
}