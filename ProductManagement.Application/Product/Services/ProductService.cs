using ProductManagement.Application.Common.Exeptions;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.ExternalServices;
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

        private readonly IProductApiClient _productApiClient;

        /// <summary>
        /// inicializador de clase <class>ProductService</class>
        /// </summary>
        /// <param name="productRepository">IProductRepository</param>
        /// <param name="productApiClient">IProductRepository</param>
        public ProductService(IProductRepository productRepository, IProductApiClient productApiClient)
        {
            _productRepository = productRepository;
            _productApiClient = productApiClient;
        }
        public async Task<Products> CreateAsync(ProductsRequestDto productsRequestDto)
        {
            var discount = await _productApiClient.GetDataItemAsync(1);
            var product = (Products)productsRequestDto;
            product.Price = productsRequestDto.Price * (100 - discount.Discount) / 100;
            return await _productRepository.CreateAsync(product);
        }
        /// <inheritdoc/>
        public async Task<ProductsDto> GetByIdAsync(int productId)
        {
            var productInfomation = await ProductExists(productId);
            return (ProductsDto)productInfomation;
        }
        /// <inheritdoc/>
        public async Task<Products> UpdateAsync(int productId, ProductsRequestDto productsRequestDto)
        {
            await ProductExists(productId);
            var discount = await _productApiClient.GetDataItemAsync(productId);
            var productUpdate = (Products)productsRequestDto;
            productUpdate.Discount = productsRequestDto.Price * (100 - discount.Discount) / 100;
            return await _productRepository.UpdateAsync(productId, productUpdate);
        }
        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(int productId)
        {
            await ProductExists(productId);
            return await _productRepository.RemoveAsync(productId);
        }

        protected async Task<Products> ProductExists(int productId)
        {
            var productInfomation = await _productRepository.GetByIdAsync(productId);
            if (productInfomation is null)
            {
                throw new NotFoundException("No existe informacion relacionados con el producto");
            }
            else
            {
                return productInfomation;
            }
        }
    }
}
