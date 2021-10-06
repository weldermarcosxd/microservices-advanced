using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task CreateProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.InsertOneAsync(product, null, cancellationToken);
        }

        public async Task<bool> DeleteProductAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var deletedResult = await _context.Products
                .DeleteOneAsync(filter, null, cancellationToken);

            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > decimal.Zero;
        }

        public async Task<Product> GetProductAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products
                .Find(filter)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products
                .Find(filter)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.Products
                .Find(x => true)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            var updateResult = await _context.Products
                .ReplaceOneAsync(filter: x => x.Id == product.Id, replacement: product, cancellationToken: cancellationToken);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > decimal.Zero;
        }
    }
}