using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;

namespace ProductManagement.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {

        public ProductRepository(ProductProductManagementDbContext productProductManagementDbContext)
        {

        }
        public async Task<Products> GetByIdAsync(int v)
        {
            return new Products();
        }
    }
}
