using Catalog.API.ViewModel;
using Catalog.API.ViewModel.Filters;
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

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromQuery] Guid productId)
    {
        if (productId != Guid.Empty)
            return BadRequest();

        var product = await productSet
            .AsNoTracking()
            .Select(productEntity => productEntity.ToProduct())
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("search/{name:minlength(1)}")]
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchProductByNameAsync(
        [FromQuery] string name,
        [FromQuery] PaginationFilter pagination)
    {
        var query = productSet
            .AsNoTracking()
            .Where(p => p.Name.StartsWith(name));

        var totalItems = await query.LongCountAsync();

        var productsOnPage = await query
            .AsNoTracking()
            .Skip(pagination.PageSize * pagination.PageIndex)
            .Take(pagination.PageSize)
            .Select(productEntity => productEntity.ToProduct())
            .ToListAsync();

        var model = new PaginatedProductsViewModel<Product>(pagination.PageIndex, pagination.PageSize, totalItems, productsOnPage);

        return Ok(model);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] ProductFilter filter,
        [FromQuery] PaginationFilter pagination)
    {
        var products = productSet
            .Include(p => p.CategoryEntity)
            .Include(p => p.BrandEntity)
            .AsNoTracking()
            .AsQueryable();

        if (filter.CategoryId != null)
            products = products.Where(p => p.CategoryId == filter.CategoryId);

        if (filter.BrandIds != null && filter.BrandIds.Any())
            products = products.Where(p => filter.BrandIds.Contains(p.BrandId));

        if (filter.MinPrice != null && filter.MaxPrice != null)
            products = products.Where(p => p.Price >= filter.MinPrice && p.Price <= filter.MaxPrice);
        else if (filter.MinPrice != null)
            products = products.Where(p => p.Price >= filter.MinPrice);
        else if (filter.MaxPrice != null)
            products = products.Where(p => p.Price <= filter.MaxPrice);

        var count = products.Count();

        if (pagination.PageIndex * pagination.PageSize >= count)
            return BadRequest();

        var data = await products
            .Skip(pagination.PageIndex * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => x.ToProduct())
            .ToListAsync();

        var paginatedProduct = new PaginatedProductsViewModel<Product>(pagination.PageIndex, pagination.PageSize, count, data);

        return Ok(paginatedProduct);
    }
}