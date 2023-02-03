using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.DTO;

public record ProductImage
{
    [DefaultValue("https://blob.com/Phone5Image-1.png")]
    public string ImageUrl { get; init; }

    [DefaultValue(true)]
    public bool IsMain { get; init; }
}

public static class ProductImageExtensions
{
    public static ProductImage ToDTO(this ProductImageEntity entity)
    {
        return new ProductImage
        {
            ImageUrl = entity.ImageUrl,
            IsMain = entity.IsMain
        };
    }
}