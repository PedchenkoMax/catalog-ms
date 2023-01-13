using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly DbSet<BrandEntity> brandSet;

    public BrandController(DbSet<BrandEntity> brandSet)
    {
        this.brandSet = brandSet;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BrandEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BrandEntity>>> GetAll()
    {
        var brands = await brandSet.ToListAsync();

        return Ok(brands);
    }
}