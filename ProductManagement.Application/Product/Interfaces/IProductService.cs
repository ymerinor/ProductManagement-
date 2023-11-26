using ProductManagement.Application.Product.Dto;
using ProductManagement.Domain.Product;

namespace ProductManagement.Application.Product.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones disponibles para el servicio de productos.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Crea un nuevo producto de manera asincrónica basado en la información proporcionada en el objeto ProductsRequestDto.
        /// </summary>
        /// <param name="productsRequestDto">Objeto que contiene la información del nuevo producto.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es el producto recién creado.</returns>
        Task<Products> CreateAsync(ProductsRequestDto productsRequestDto);

        /// <summary>
        /// Obtiene un objeto Products por su identificador de manera asincrónica.
        /// </summary>
        /// <param name="productId">Identificador del producto.</param>
        /// <returns>Un objeto Products que corresponde al identificador proporcionado.</returns>
        Task<Products> GetByIdAsync(int productId);
        Task<Products> UpdateAsync(ProductsRequestDto productsRequestDto);
    }
}