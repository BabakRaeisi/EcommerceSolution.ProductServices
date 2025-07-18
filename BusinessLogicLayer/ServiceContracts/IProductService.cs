
using BusinessLogicLayer.DTO;
using DataAccessLayer.Entities;
using System.Linq.Expressions;


namespace BusinessLogicLayer.ServiceContracts;

public  interface IProductService
{
    /// <summary>
    /// returns a list of products from the repository
    /// </summary>
    /// <returns></returns>
    Task<List<ProductResponse?>> GetProducts();

    /// <summary>
    /// get list of product by specified condition
    /// </summary>
    /// <param name="ConditionExpression"></param>
    /// <returns></returns>
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func
        <Product, bool>>ConditionExpression);
    /// <summary>
    /// // get a single product by specified condition
    /// </summary>
    /// <param name="ConditionExpression"></param>
    /// <returns></returns>
    Task<ProductResponse?>GetProductByCondition(Expression<Func<Product, bool>> ConditionExpression);


    /// <summary>
    /// add a new product to the repository
    /// </summary>
    /// <param name="productAddRequest"></param>
    /// <returns></returns>
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);

    /// <summary>
    /// update an existing product in the repository
    /// </summary>
    /// <param name="productUpdateRequest"></param>
    /// <returns></returns>
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);

    /// <summary>
    /// Delete an existinig product from the repository
    /// </summary>
    /// <returns> returns true if successful</returns>
    Task<bool> DeleteProduct(Guid productId);
}
