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
    [ProducesResponseType(typeof(PaginatedList<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProductsByParametersAsync(
        [FromQuery] SearchParameters search,
        [FromQuery] FilteringParameters filter,
        [FromQuery] OrderingParameters ordering,
        [FromQuery] PaginationParameters pagination)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var products = productSet
            .Include(p => p.CategoryEntity)
            .Include(p => p.BrandEntity)
            .AsNoTracking()
            .AsQueryable();

        products.ApplySearch(search);
        products.ApplyFilter(filter);
        products.ApplyOrder(ordering);
        products.ApplyPagination(pagination);

        var res = await products
            .Select(x => x.ToProduct())
            .ToListAsync();

        return res.Count == 0
            ? NotFound()
            : Ok(res);
    }
}