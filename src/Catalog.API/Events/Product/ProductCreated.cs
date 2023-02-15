using Catalog.API.Events.Abstractions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;

namespace Catalog.API.Events.Product;

public record ProductCreatedEvent(
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

public class ProductCreatedEventConsumer : BaseCreatedEventConsumer<ProductCreatedEvent, ProductEntity>
{
    public ProductCreatedEventConsumer(ILogger<ProductCreatedEventConsumer> logger, CatalogContext ctx) :
        base(logger, ctx)
    {
    }
}