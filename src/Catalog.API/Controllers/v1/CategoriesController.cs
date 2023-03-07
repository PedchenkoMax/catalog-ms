using Catalog.API.DTO;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly DbSet<CategoryEntity> categorySet;

    public CategoriesController(CatalogContext context)
    {
        categorySet = context.Categories;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
    {
        var categories = await categorySet
            .AsNoTracking()
            .Select(categoryEntity => categoryEntity.ToDTO())
            .ToListAsync();

        return Ok(categories);
    }

    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid categoryId) {
        if (categoryId == Guid.Empty)
            return BadRequest();

        var category = await categorySet
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == categoryId);

        if (category == null)
            return NotFound();

        return Ok(category.ToDTO());
    }
}