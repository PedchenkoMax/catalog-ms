using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Database.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid productId);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAllByCategoryAsync(int categoryId);
}