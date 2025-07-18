using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;
 
namespace BusinessLogicLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
        private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;


        public ProductService(
            IValidator<ProductAddRequest> productAddRequestValidator,
            IValidator<ProductUpdateRequest> productUpdateRequestValidator,
            IMapper mapper,
            IProductRepository productsRepository)
        {
            _productAddRequestValidator = productAddRequestValidator;
            _productUpdateRequestValidator = productUpdateRequestValidator;
            _mapper = mapper;
            _productRepository = productsRepository;
        }

        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            if  (productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest), "Product add request cannot be null.");
            }
            ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
               string.Join(",", validationResult.Errors.Select(temp=>temp.ErrorMessage));//Error1 , Error2, Error3
                throw new ValidationException(validationResult.Errors);
            }


           Product productInput= _mapper.Map<Product>(productAddRequest);
           Product? addedProdutc = await _productRepository.AddProduct(productInput);
            if (addedProdutc == null)
            {
               return null; 
            }
            ProductResponse addedProductResponse=  _mapper.Map<ProductResponse>(addedProdutc);

            return addedProductResponse;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
           Product? existingProduct= await _productRepository.GetProductByCondition(temp => temp.ProductID == productId);
            if (existingProduct==null)
            {
                return false;
            }
            bool isDeleted =await _productRepository.DeleteProduct(productId);

            return isDeleted;
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> ConditionExpression)
        {
            Product? product = await _productRepository.GetProductByCondition(ConditionExpression);

            if (product == null)
            {
                return null;
            }
            ProductResponse productResponse = _mapper.Map<ProductResponse>(product);

            return productResponse;
        }

        public async Task<List<ProductResponse?>> GetProducts()
        {
            IEnumerable<Product?> products = await _productRepository.GetProducts( );

          
            IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return productResponses.ToList();
        }

        public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> ConditionExpression)
        {
            IEnumerable<Product?> products = await _productRepository.GetProductsByCondition(ConditionExpression);


            IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return productResponses.ToList();
        }

        public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
          Product? existingProduct=  await _productRepository.GetProductByCondition
                (temp => temp.ProductID == productUpdateRequest.ProductId);

            if (existingProduct == null)
                throw new ArgumentException("Product not found.", nameof(productUpdateRequest.ProductId));

           ValidationResult result =await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

            if (!result.IsValid)
            {
                string errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessage);
            }
            // Map the update request to the existing product
            Product product =_mapper.Map<Product>(productUpdateRequest);
           
            Product? updatedProduct = await _productRepository.UpdateProduct(product);
            
            ProductResponse? updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct);

            return updatedProductResponse;
        }
    }
}
