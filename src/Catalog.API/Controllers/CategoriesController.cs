using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly DbSet<CategoryEntity> categorySet;

    public CategoriesController(DbSet<CategoryEntity> categorySet)
    {
        this.categorySet = categorySet;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
    {
        var categories = await categorySet
            .AsNoTracking()
            .Select(categoryEntity => categoryEntity.ToCategory())
            .ToListAsync();

        return Ok(categories);
    }
}