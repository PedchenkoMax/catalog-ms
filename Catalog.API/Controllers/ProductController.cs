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
    [ProducesResponseType(typeof(IEnumerable<ProductEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductEntity>>> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var totalProduct = await productSet.LongCountAsync();

        var productOnPage = await productSet
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        var model = new PaginatedProductsViewModel<ProductEntity>(pageIndex, pageSize, totalProduct, productOnPage);

        return Ok(model);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProductEntity), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductEntity>> GetById(Guid productId)
    {
        if (productId != Guid.Empty)
            return BadRequest();

        var product = await productSet.FirstOrDefaultAsync(x => x.ProductId == productId);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProductEntity), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductEntity>>> GetByCategoryIdAndBrandIdAsync(Guid categoryId, Guid? brandId)
    {
        IEnumerable<ProductEntity> products = await productSet
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();

        if (brandId.HasValue)
            products = products.Where(p => p.BrandId == brandId);

        return Ok(products);
    }
}