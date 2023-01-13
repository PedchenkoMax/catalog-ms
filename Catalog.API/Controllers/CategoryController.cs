using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly DbSet<CategoryEntity> categorySet;

    public CategoryController(DbSet<CategoryEntity> categorySet)
    {
        this.categorySet = categorySet;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryEntity>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryEntity>>> GetAll()
    {
        var categories = await categorySet.ToListAsync();

        return Ok(categories);
    }
}