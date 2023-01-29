using System.ComponentModel;

namespace Catalog.API.ViewModel;

public record ProductImage
{
    [DefaultValue("https://blobstorage.com/default-image.jpg")]
    public string ImageUrl { get; init; }

    [DefaultValue(true)]
    public bool IsMain { get; init; }
}