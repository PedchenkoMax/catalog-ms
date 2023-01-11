using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Database.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext ctx;
    private readonly DbSet<Product> products;

    public ProductRepository(CatalogContext ctx)
    {
        this.ctx = ctx;
        products = ctx.Products;
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        return await products
            .FirstOrDefaultAsync(x => x.ProductId == productId);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await products
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllByCategoryAsync(int categoryId)
    {
        return await products
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();
    }
}