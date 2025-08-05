using DataAccessLayer.Entities; 
using System.Linq.Expressions;

namespace DataAccessLayer.RepositoryContracts;
/// <summary>
/// repository contract for Product entity
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Retrieves a collection of products asynchronously.
    /// </summary>
    /// <remarks>This method returns all available products. The returned collection may be empty if no
    /// products are available.</remarks>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/> of
    /// <see cref="Product"/> objects.</returns>
    public Task<IEnumerable<Product>> GetProducts();

    /// <summary>
    /// retrieves a product by specified condition asynchronously.
    /// </summary>
    /// <param name="ConditionExpression"></param>
    /// <returns></returns>
    public Task<IEnumerable<Product?>>GetProductsByCondition(Expression<Func<Product, bool>> ConditionExpression);
    /// <summary>
    /// gets a product by specified condition asynchronously.
    /// </summary>
    /// <param name="ConditionExpression"></param>
    /// <returns></returns>
    public Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> ConditionExpression);

    /// <summary>
    ///  Adds a new product to the repository asynchronously.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public Task<Product?> AddProduct(Product product);
    /// <summary>
    /// updates an existing product in the repository asynchronously.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public Task<Product?> UpdateProduct(Product product);
    /// <summary>
    /// deletes a product by its ID asynchronously.
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Task<bool> DeleteProduct(Guid productId);

}
