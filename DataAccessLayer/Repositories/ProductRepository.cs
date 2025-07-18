using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context ;
        }
        public async Task<Product?> AddProduct(Product product)
        {
           _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            Product? existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.ProductID == productId);

            if (existingProduct == null)return false;
            _context.Products.Remove(existingProduct);
            int affectedRows = await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return affectedRows>0;
        }

        public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> ConditionExpression)
        {
            return await _context.Products.
                FirstOrDefaultAsync(ConditionExpression);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> ConditionExpression)
        {
            return await _context.Products.
                Where(ConditionExpression).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
           Product? existingProduct =_context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct == null)
            {
                return  null;
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Category = product.Category;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.QuantityInStock = product.QuantityInStock;
            
            await _context.SaveChangesAsync();
            return existingProduct;

        }
    }
}
