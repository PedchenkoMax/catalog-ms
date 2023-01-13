using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly CatalogContext catalogContext;

    public CategoryController(CatalogContext catalogContext)
    {
        this.catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
    }

    [HttpGet] [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await catalogContext.Category
            .ToListAsync();

        return Ok(categories);
    }
}