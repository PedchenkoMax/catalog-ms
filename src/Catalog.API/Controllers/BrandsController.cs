using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly DbSet<BrandEntity> brandSet;

    public BrandsController(CatalogContext context)
    {
        brandSet = context.Brands;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrandsAsync()
    {
        var brands = await brandSet
            .AsNoTracking()
            .Select(brandEntity => brandEntity.ToDTO())
            .ToListAsync();

        return Ok(brands);
    }

    [MapToApiVersion("2.0")]
    [HttpGet("{brandId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Brand), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBrandById([FromRoute] Guid brandId) {
        if (brandId == Guid.Empty)
            return BadRequest();

        var brand = await brandSet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == brandId);

        if (brand == null)
            return NotFound();

        return Ok(brand.ToDTO());
    }
}