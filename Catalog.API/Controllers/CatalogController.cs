using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {         
        private readonly CatalogContext catalogContext;

        public CatalogController(CatalogContext catalogContext)
        {                      
            this.catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
        }
        
        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> ProductsAsync()
        {
            var products = await catalogContext.Products
                .ToListAsync();

            return Ok(products);
        }
        
        [HttpGet]
        [Route("products/{productId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> ProductByIdAsync(Guid productId)
        {
            if (productId != Guid.Empty)
            {
                return BadRequest();
            }

            var product = await catalogContext.Products
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }     

        [Route("products/category/{categoryId}/brand/{brandId:int?}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]        
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> ProductsByCategoryIdAndBrandIdAsync(int categoryId, int? brandId)
        {    

            if (categoryId <= 0)
            {
                return BadRequest();
            }

            IEnumerable<Product> products = await catalogContext.Products
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();

            if (brandId.HasValue)
            {
                products = products.Where(p => p.BrandId == brandId);
            }                     

            return Ok(products);
        }

        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Category>>> CategoriesAsync()
        {
            var categories = await catalogContext.Category
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet]
        [Route("brends")]
        [ProducesResponseType(typeof(IEnumerable<Brand>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Brand>>> BrandsAsync()
        {
            var brands = await catalogContext.Brand
                .ToListAsync();

            return Ok(brands);
        }
    }
}
