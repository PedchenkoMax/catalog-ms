using Catalog.API.DTO;
using Catalog.API.DTO.Filters;
using Catalog.API.QueryableExtensions;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly DbSet<ProductEntity> productSet;

    public ProductsController(CatalogContext context)
    {
        productSet = context.Products;
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
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images)
            .AsNoTracking()
            .Select(productEntity => productEntity.ToDTO())
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PaginatedList<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProductsByParametersAsync(
        [FromQuery] ProductFilter filter,
        [FromQuery] SearchFilter search,
        [FromQuery] OrderFilter ordering,
        [FromQuery] PaginationFilter pagination)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var products = productSet
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Images)
            .AsNoTracking()
            .AsQueryable();

        products = products.ApplyFilter(filter);
        products = products.ApplySearch(search);
        products = products.ApplyOrder(ordering);
        products = products.ApplyPagination(pagination);

        var res = await products
            .Select(x => x.ToDTO())
            .ToListAsync();

        return res.Count == 0
            ? NotFound()
            : Ok(res);
    }
}