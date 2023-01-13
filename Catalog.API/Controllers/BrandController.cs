using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly CatalogContext catalogContext;

    public BrandController(CatalogContext catalogContext)
    {
        this.catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Brand>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAll()
    {
        var brands = await catalogContext.Brand
            .ToListAsync();

        return Ok(brands);
    }
}