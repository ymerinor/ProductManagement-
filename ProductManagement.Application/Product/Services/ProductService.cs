using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;

namespace ProductManagement.Application.Product.Services
{
    /// <inheritdoc/>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Dependencia de IProductRepository
        /// </summary>
        public readonly IProductRepository _productRepository;

        /// <summary>
        /// inicializador de clase <class>ProductService</class>
        /// </summary>
        /// <param name="productRepository">IProductRepository</param>
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Products> CreateAsync(ProductsRequestDto productsRequestDto)
        {
            var discount = 0M;
            var product = (Products)productsRequestDto;
            product.Discount = productsRequestDto.Price * (discount - 100) / 100;
            return await _productRepository.CreateAsync(product);
        }

        /// <inheritdoc/>
        public async Task<Products> GetByIdAsync(int v)
        {
            return await _productRepository.GetByIdAsync(v);
        }

        public Task<Products> UpdateAsync(ProductsRequestDto productsRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
