using ProductManagement.Application.Product.Dto;
using ProductManagement.Domain.Product;

namespace ProductManagement.Application.Product.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones disponibles para el servicio de productos.
    /// </summary>
    public interface IProductService
    {
        Task<Products> CreateAsync(ProductsRequestDto productsDto);

        /// <summary>
        /// Obtiene un objeto Products por su identificador de manera asincrónica.
        /// </summary>
        /// <param name="productId">Identificador del producto.</param>
        /// <returns>Un objeto Products que corresponde al identificador proporcionado.</returns>
        Task<Products> GetByIdAsync(int productId);
    }
}