using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;

namespace ProductManagement.Infrastructure.Repository
{
    /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<Products> GetByIdAsync(int productId)
        {
            return _context.Products.FirstOrDefault(t => t.ProductId == productId);
        }
    }
}
