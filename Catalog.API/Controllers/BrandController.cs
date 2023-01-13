using Catalog.API.ViewModel;
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
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
    {
        var brands = await brandSet
            .Select(brandEntity => new Brand
            {
                BrandId = brandEntity.BrandId,
                Name = brandEntity.Name
            })
            .ToListAsync();

        return Ok(brands);
    }
}