using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.Product;

namespace ProductManagement.Application.Product.Services
{
    public class ProductService : IProductService
    {
        public ProductService()
        {

        }
        public Task<IEnumerable<Products>> GetByIdAsync(int v)
        {
            throw new NotImplementedException();
        }
    }
}
