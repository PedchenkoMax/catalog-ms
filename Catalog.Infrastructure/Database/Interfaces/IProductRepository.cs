using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Database.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid productId);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);

    //Task<IEnumerable<Category>> GetCategoriesAsync();

    
}