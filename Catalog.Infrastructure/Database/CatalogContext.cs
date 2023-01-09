#pragma warning disable CS8618

using System.Reflection;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Category { get; set; }
}