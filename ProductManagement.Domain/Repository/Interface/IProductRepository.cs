using ProductManagement.Domain.Product;

namespace ProductManagement.Domain.Repository.Interface
{
    /// <summary>
    /// Interfaz que define las operaciones de acceso a datos para la entidad de productos.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Obtiene un producto por su identificador de manera asincrónica.
        /// </summary>
        /// <param name="productId">Identificador del producto.</param>
        /// <returns>Un objeto Products que corresponde al identificador proporcionado.</returns>
        Task<Products> GetByIdAsync(int productId);
    }
}
