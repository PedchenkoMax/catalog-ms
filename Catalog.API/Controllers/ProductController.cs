using Catalog.API.ViewModel;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly CatalogContext catalogContext;

    public ProductController(CatalogContext catalogContext)
    {
        this.catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalProduct = await catalogContext.Products
            .LongCountAsync();

        var productOnPage = await catalogContext.Products
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var model = new PaginatedProductsViewModel<Product>(pageIndex, pageSize, totalProduct, productOnPage);

        return Ok(model);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<ActionResult<Product>> GetById(Guid productId)
    {
        if (productId != Guid.Empty)
        {
            return BadRequest();
        }

        var product = await catalogContext.Products
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryIdAndBrandIdAsync(Guid categoryId, Guid? brandId)
    {
        IEnumerable<Product> products = await catalogContext.Products
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();

        if (brandId.HasValue)
        {
            products = products.Where(p => p.BrandId == brandId);
        }

        return Ok(products);
    }
}