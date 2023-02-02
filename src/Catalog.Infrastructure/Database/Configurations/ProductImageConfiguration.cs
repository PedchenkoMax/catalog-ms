using Catalog.Domain.Constants;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Database.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImageEntity>
{
    public void Configure(EntityTypeBuilder<ProductImageEntity> builder)
    {
        builder.HasKey(x => x.ProductImageId);


        SeedProductImage(builder, SeedDataConstants.Phone1, 5, "Phone1Image");
        SeedProductImage(builder, SeedDataConstants.Phone2, 3, "Phone2Image");
        SeedProductImage(builder, SeedDataConstants.Phone3, 12, "Phone3Image");
        SeedProductImage(builder, SeedDataConstants.Phone4, 15, "Phone4Image");
        SeedProductImage(builder, SeedDataConstants.Phone5, 3, "Phone5Image");

        SeedProductImage(builder, SeedDataConstants.Tv1, 4, "Tv1Image");
        SeedProductImage(builder, SeedDataConstants.Tv2, 6, "Tv2Image");
        SeedProductImage(builder, SeedDataConstants.Tv3, 1, "Tv3Image");
        SeedProductImage(builder, SeedDataConstants.Tv4, 9, "Tv4Image");
        SeedProductImage(builder, SeedDataConstants.Tv5, 8, "Tv5Image");
    }

    private static void SeedProductImage(EntityTypeBuilder<ProductImageEntity> builder,
        Guid productId, int count, string imagePrefix)
    {
        for (var i = 1; i <= count; i++)
            builder.HasData(new ProductImageEntity
            {
                ProductImageId = Guid.NewGuid(),
                ImageUrl = $"https://blob.com/{imagePrefix}-{i}.png",
                IsMain = i == 1,
                ProductId = productId
            });
    }
}