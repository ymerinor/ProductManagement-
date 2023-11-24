using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;

namespace ProductManagement.Application.Product.Services
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Products> GetByIdAsync(int v)
        {
            return await _productRepository.GetByIdAsync(v);
        }
    }
}
