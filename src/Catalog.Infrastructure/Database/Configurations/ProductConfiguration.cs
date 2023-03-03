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
    }
}