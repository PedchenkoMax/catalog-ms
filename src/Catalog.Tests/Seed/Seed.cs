using Bogus;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Catalog.Tests.LoadTests;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Tests.Seed;

public static class Seed
{
    private const string ConnectionString = "Server=localhost;Database=LoadTestDb;User=sa;Password=KtPm23GLP@ssW0rd!;Trust Server Certificate=true;Trusted_Connection=true;Persist Security Info=true;";

    private static readonly DbContextOptions<CatalogContext> Options = new DbContextOptionsBuilder<CatalogContext>().UseSqlServer(ConnectionString).Options;

    public static void SeedDb()
    {
        Randomizer.Seed = new Random(42);
        var faker = new Faker();

        var ctx = new CatalogContext(Options);

        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();

        ctx.Brands.AddRange(Brands.All);
        ctx.Categories.AddRange(Categories.All);

        var imageFaker = new Faker<ProductImageEntity>()
            .RuleFor(i => i.Id, _ => Guid.NewGuid())
            .RuleFor(i => i.ImageUrl, _ => "blob.com/gl-survivors/proudctImage/")
            .FinishWith((_, i) => i.ImageUrl += i.Id + ".png");

        var products = new List<ProductEntity>();
        foreach (var (category, brands) in CategoryBrands.Dictionary)
        {
            foreach (var brand in brands)
            {
                var productFaker = new Faker<ProductEntity>()
                    .RuleFor(b => b.Id, _ => Guid.NewGuid())
                    .RuleFor(b => b.Name, f => $"{brand.Name} {f.PickRandom(ProductNameTagsPerCategory.Dictionary[category])}")
                    .RuleFor(b => b.Description, f => f.Lorem.Paragraphs(1, 5))
                    .RuleFor(b => b.FullPrice, f => f.Random.Decimal(500, 15000))
                    .RuleFor(b => b.Discount, f => f.Random.Decimal(-500, 2500))
                    .RuleFor(b => b.Quantity, f => f.Random.Int(-10, 42))
                    .RuleFor(b => b.Category, _ => category)
                    .RuleFor(b => b.CategoryId, _ => category.Id)
                    .RuleFor(b => b.Brand, _ => brand)
                    .RuleFor(b => b.BrandId, _ => brand.Id)
                    .RuleFor(b => b.Images, _ => imageFaker.Generate(faker.Random.Int(1, 10)))
                    .FinishWith((f, b) =>
                    {
                        b.FullPrice = Math.Ceiling(b.FullPrice);
                        var validDiscount = b.Discount < 42 ? 0 : b.Discount;
                        b.Discount = b.FullPrice < (validDiscount * 1.42M) ? 0 : Math.Ceiling(validDiscount);

                        b.Quantity = b.Quantity < 0 ? 0 : b.Quantity;

                        b.IsActive = b.Quantity > 0;

                        foreach (var image in b.Images!)
                        {
                            image.ProductId = b.Id;
                        }

                        b.Images.First().IsMain = true;
                    });

                products.AddRange(productFaker.Generate(faker.Random.Int(0, 42)));
            }
        }

        ctx.Products.AddRange(products);

        ctx.SaveChanges();
    }
}