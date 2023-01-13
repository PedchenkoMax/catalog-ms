using Catalog.API.ViewModel;
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
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        var categories = await categorySet
            .Select(categoryEntity => new Category
            {
                CategoryId = categoryEntity.CategoryId,
                Name = categoryEntity.Name
            })
            .ToListAsync();

        return Ok(categories);
    }
}