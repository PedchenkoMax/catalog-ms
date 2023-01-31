using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : ControllerBase
{
    private readonly DbSet<BrandEntity> brandSet;

    public BrandsController(DbSet<BrandEntity> brandSet)
    {
        this.brandSet = brandSet;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrandsAsync()
    {
        var brands = await brandSet
            .AsNoTracking()
            .Select(brandEntity => brandEntity.ToBrand())
            .ToListAsync();

        return Ok(brands);
    }
}