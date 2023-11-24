using ProductManagement.Domain.Product;

namespace ProductManagement.Application.Product.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetByIdAsync(int v);
    }
}