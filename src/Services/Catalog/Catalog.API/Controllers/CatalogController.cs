using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductAsync(string id, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductAsync(id, cancellationToken);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("[action]/{categoryName}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategoryAsync(string categoryName, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductByCategoryAsync(categoryName, cancellationToken);
            return Ok(products);
        }

        [HttpGet("[action]/{productName}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByNameAsync(string productName, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductByNameAsync(productName, cancellationToken);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<IEnumerable<Product>>> CreateProductAsync([FromBody] Product product, CancellationToken cancellationToken)
        {
            await _repository.CreateProductAsync(product, cancellationToken);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] Product product, CancellationToken cancellationToken)
        {
            return Ok(await _repository.UpdateProductAsync(product, cancellationToken));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteProductAsync(string id, CancellationToken cancellationToken)
        {
            return Ok(await _repository.DeleteProductAsync(id, cancellationToken));
        }
    }
}