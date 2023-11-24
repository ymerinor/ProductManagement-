using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;

namespace ProductManagement.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductManagementDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProductRepository(ProductManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Products> GetByIdAsync(int v)
        {
            return new Products();
        }
    }
}
