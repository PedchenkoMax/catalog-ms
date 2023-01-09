using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Database.Repositories;

public class ProductRepository
{
    private readonly CatalogContext ctx;

    public ProductRepository(CatalogContext ctx)
    {
        this.ctx = ctx;
    }

    public List<Product> GetAll()
    {
        return ctx.Products.ToList();
    }

    public Product Get(Guid productId)
    {
        return ctx.Products
            .First(x => x.ProductId == productId);
    }

    public int Add(Product newProduct)
    {
        ctx.Products.Add(newProduct);

        return ctx.SaveChanges();
    }

    public int Remove(Product product)
    {
        ctx.Products.Remove(product);

        return ctx.SaveChanges();
    }
}