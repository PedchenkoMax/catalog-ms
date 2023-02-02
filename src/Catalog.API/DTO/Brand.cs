using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.DTO;

public record Brand
{
    [DefaultValue("00000000-0000-0000-0000-000000000000")]
    public Guid BrandId { get; init; }

    [DefaultValue("Unnamed Brand")]
    public string Name { get; init; }

    [DefaultValue("https://blobstorage.com/default-image.jpg")]
    public string Image { get; init; }
}

public static class BrandExtensions
{
    public static Brand ToDTO(this BrandEntity entity)
    {
        return new Brand
        {
            BrandId = entity.BrandId,
            Name = entity.Name,
            Image = entity.Image
        };
    }
}