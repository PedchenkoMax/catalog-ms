using Catalog.API.ViewModel;
using Catalog.API.ViewModel.Parameters;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly DbSet<ProductEntity> productSet;

    public ProductsController(DbSet<ProductEntity> productSet)
    {
        this.productSet = productSet;
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProductByIdAsync([FromQuery] Guid productId)
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedProductsViewModel<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProductsByParametersAsync(
        [FromQuery] FilteringParameters filter,
        [FromQuery] PaginationParameters pagination,
        [FromQuery] OrderingParameters ordering,
        [FromQuery] SearchParameters search)
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
            (filter.MinPrice == null || p.FullPrice >= filter.MinPrice) &&
            (filter.MaxPrice == null || p.FullPrice <= filter.MaxPrice));

        if (!string.IsNullOrEmpty(search.Query))
            products = products.Where(p => p.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase));

        if (ordering.OrderBy == "Price")
            products = ordering.Desc
                ? products.OrderByDescending(p => p.FullPrice)
                : products.OrderBy(p => p.FullPrice);

        var totalNumberOfProducts = products.Count();

        if (pagination.PageIndex * pagination.PageSize >= totalNumberOfProducts)
            return NotFound();

        var productsOnPage = await products
            .Skip(pagination.PageIndex * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => x.ToProduct())
            .ToListAsync();

        var paginatedProduct = new PaginatedProductsViewModel<Product>(
            pagination.PageIndex,
            pagination.PageSize,
            totalNumberOfProducts,
            productsOnPage);

        return Ok(paginatedProduct);
    }
}