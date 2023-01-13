using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly DbSet<Brand> brandSet;

    public BrandController(DbSet<Brand> brandSet)
    {
        this.brandSet = brandSet;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
    {
        var brands = await brandSet.ToListAsync();

        return Ok(brands);
    }
}