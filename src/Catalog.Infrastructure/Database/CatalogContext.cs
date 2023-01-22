#pragma warning disable CS8618

using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Database;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<BrandEntity> Brand { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        // builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
    }
}