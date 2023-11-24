using ProductManagement.Domain.Product;

namespace ProductManagement.Domain.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Products> GetByIdAsync(int v);
    }
}
