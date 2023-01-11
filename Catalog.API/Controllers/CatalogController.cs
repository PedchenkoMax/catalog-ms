using Catalog.Domain.Entities;
using Catalog.Infrastructure.Database.Interfaces;
using Catalog.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository repository;

        public CatalogController(IProductRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));           
        }

        [HttpGet]
        [Route("products")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> ProductsAsync()
        {
            var products = await repository.GetProductsAsync();
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

            var product = await repository.GetProductByIdAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }

        [HttpGet]
        [Route("category/{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> ProductsByCategoryIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest();
            }

            var product = await repository.GetProductsByCategoryIdAsync(categoryId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }
    }
}
