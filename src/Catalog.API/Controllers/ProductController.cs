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
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts(
        [FromQuery] ProductFilter filter,
        [FromQuery] PaginationFilter pagination,
        [FromQuery] OrderingFilter ordering,
        [FromQuery] SearchFilter search)
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

        products = products.Where(p =>
            (filter.MinPrice == null || p.Price >= filter.MinPrice) &&
            (filter.MaxPrice == null || p.Price <= filter.MaxPrice));

        if (!string.IsNullOrEmpty(search.Query))
            products = products.Where(p => p.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase));

        products = ordering.OrderBy switch
        {
            "Name" => ordering.Desc
                ? products.OrderByDescending(p => p.Name)
                : products.OrderBy(p => p.Name),

            "Price" => ordering.Desc
                ? products.OrderByDescending(p => p.Price)
                : products.OrderBy(p => p.Price),

            _ => ordering.Desc
                ? products.OrderByDescending(p => p.ProductId)
                : products.OrderBy(p => p.ProductId)
        };

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