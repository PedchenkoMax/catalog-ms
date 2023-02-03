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

        SeedProductImage(builder, SeedDataConstants.Notebook1, 5, "Notebook1Image");
        SeedProductImage(builder, SeedDataConstants.Notebook2, 4, "Notebook2Image");
        SeedProductImage(builder, SeedDataConstants.Notebook3, 2, "Notebook3Image");
        SeedProductImage(builder, SeedDataConstants.Notebook4, 12, "Notebook4Image");
        SeedProductImage(builder, SeedDataConstants.Notebook5, 6, "Notebook5Image");
        SeedProductImage(builder, SeedDataConstants.Notebook6, 7, "Notebook6Image");
        SeedProductImage(builder, SeedDataConstants.Notebook7, 8, "Notebook7Image");
        SeedProductImage(builder, SeedDataConstants.Notebook8, 12, "Notebook8Image");
        SeedProductImage(builder, SeedDataConstants.Notebook9, 14, "Notebook9Image");
        SeedProductImage(builder, SeedDataConstants.Notebook10, 15, "Notebook10Image");
        SeedProductImage(builder, SeedDataConstants.Notebook11, 1, "Notebook11Image");
        SeedProductImage(builder, SeedDataConstants.Notebook12, 2, "Notebook12Image");
        SeedProductImage(builder, SeedDataConstants.Notebook13, 5, "Notebook13Image");
        SeedProductImage(builder, SeedDataConstants.Notebook14, 4, "Notebook14Image");
        SeedProductImage(builder, SeedDataConstants.Notebook15, 7, "Notebook15Image");
        SeedProductImage(builder, SeedDataConstants.Notebook16, 1, "Notebook16Image");
        SeedProductImage(builder, SeedDataConstants.Notebook17, 12, "Notebook17Image");
        SeedProductImage(builder, SeedDataConstants.Notebook18, 6, "Notebook18Image");
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