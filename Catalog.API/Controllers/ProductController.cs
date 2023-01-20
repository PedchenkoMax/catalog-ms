using Catalog.API.ViewModel;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        var productsOnPage = await productSet
            .Include(e => e.CategoryEntity)
            .Include(e => e.BrandEntity)
            .OrderBy(e => e.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(productEntity => productEntity.ToProduct())
            .ToListAsync();

        var model = new PaginatedProductsViewModel<Product>(pageIndex, pageSize, totalProduct, productsOnPage);

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
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("category/{categoryId:guid}/brand/{brandId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedProductsViewModel<Product>>> GetByCategoryIdAndBrandIdAsync(Guid categoryId, 
        Guid? brandId, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        if (categoryId != Guid.Empty)
            return BadRequest();

        var query = productSet
            .Where(p => p.CategoryId == categoryId);

        if (brandId.HasValue)
            query = query.Where(p => p.BrandId == brandId);

        var totalProduct = await query.LongCountAsync();

        var productsOnPage = await query         
           .OrderBy(p => p.Name)
           .Skip(pageSize * pageIndex)
           .Take(pageSize)
           .Select(productEntity => productEntity.ToProduct())
           .ToListAsync();

        var model = new PaginatedProductsViewModel<Product>(pageIndex, pageSize, totalProduct, productsOnPage);

        return Ok(model);       
    }

    [HttpGet("search/{name:minlength(1)}")]   
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginatedProductsViewModel<Product>>> SearchProductByNameAsync(string name, 
        [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var query = productSet
            .Where(p => p.Name.StartsWith(name));

        var totalItems = await query.LongCountAsync();

        var productsOnPage = await query           
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(productEntity => productEntity.ToProduct())
            .ToListAsync();        

        var model = new PaginatedProductsViewModel<Product>(pageIndex, pageSize, totalItems, productsOnPage);

        return Ok(model);
    }
}