using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Product;

public record ProductUpdatedEvent(
    Guid Id,
    string Name,
    string Description,
    decimal FullPrice,
    decimal Discount,
    int Quantity,
    bool IsActive,
    Guid CategoryId,
    Guid BrandId) : IEvent<ProductEntity>
{
    public ProductEntity ToEntity()
    {
        return new ProductEntity
        {
            Id = Id,
            Name = Name,
            Description = Description,
            FullPrice = FullPrice,
            Discount = Discount,
            Quantity = Quantity,
            IsActive = IsActive,
            CategoryId = CategoryId,
            BrandId = BrandId
        };
    }
}

public class ProductUpdatedEventConsumer : BaseUpdatedEventConsumer<ProductUpdatedEvent, ProductEntity>
{
    public ProductUpdatedEventConsumer(ILogger<ProductUpdatedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx) { }
}