using Catalog.API.ViewModel;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly DbSet<ProductEntity> productSet;

    public ProductController(DbSet<ProductEntity> productSet)
    {
        this.productSet = productSet;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedProductsViewModel<Product>>> GetAll([FromQuery] int pageSize = 10,
        [FromQuery] int pageIndex = 0)
    {
        var totalProduct = await productSet.LongCountAsync();

        var products = await productSet
            .Include(e => e.CategoryEntity)
            .Include(e => e.BrandEntity)
            .OrderBy(e => e.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(productEntity => productEntity.ToProduct())
            .ToListAsync();

        var model = new PaginatedProductsViewModel<Product>(pageIndex, pageSize, totalProduct, products);

        return Ok(model);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<ActionResult<Product>> GetById(Guid productId)
    {
        if (productId != Guid.Empty)
            return BadRequest();

        var product = await productSet
            .Select(productEntity => productEntity.ToProduct())
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("category/{categoryId:guid}/brand/{brandId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategoryIdAndBrandIdAsync(Guid categoryId, Guid? brandId)
    {
        var query = productSet
            .Where(x => x.CategoryId == categoryId);

        if (brandId.HasValue)
            query = query.Where(p => p.BrandId == brandId);

        var products = await query
            .Select(productEntity => productEntity.ToProduct())
            .ToListAsync();
        
        return Ok(products);
    }

}