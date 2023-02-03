using System.ComponentModel;
using Catalog.Domain.Entities;

namespace Catalog.API.DTO;

public record Brand
{
    [DefaultValue("d942706b-e4e2-48f9-bbdc-b022816471f0")]
    public Guid BrandId { get; init; }

    [DefaultValue("Samsung")]
    public string Name { get; init; }

    [DefaultValue("https://upload.wikimedia.org/wikipedia/commons/f/fa/Apple_logo_black.svg")]
    public string Image { get; init; }
    
    public IList<Product>? Products { get; set; }
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