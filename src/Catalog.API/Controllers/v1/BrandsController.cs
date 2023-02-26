using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
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
}