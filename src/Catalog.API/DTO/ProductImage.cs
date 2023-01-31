using System.ComponentModel;

namespace Catalog.API.DTO;

public record ProductImage
{
    [DefaultValue("https://blobstorage.com/default-image.jpg")]
    public string ImageUrl { get; init; }

    [DefaultValue(true)]
    public bool IsMain { get; init; }
}