﻿using ProductManagement.Domain.Product;

namespace ProductManagement.Domain.Repository.Interface
{
    /// <summary>
    /// Interfaz que define las operaciones de acceso a datos para la entidad de productos.
    /// </summary>
    public interface IProductRepository
    {
        // <summary>
        /// Crea un nuevo producto de manera asincrónica basado en la información proporcionada en el objeto Products.
        /// </summary>
        /// <param name="products">Objeto que contiene la información del nuevo producto.</param>
        /// <returns>Una tarea que representa la operación asincrónica. El resultado de la tarea es el producto recién creado.</returns>
        Task<Products> CreateAsync(Products products);

        /// <summary>
        /// Obtiene un producto por su identificador de manera asincrónica.
        /// </summary>
        /// <param name="productId">Identificador del producto.</param>
        /// <returns>Un objeto Products que corresponde al identificador proporcionado.</returns>
        Task<Products> GetByIdAsync(int productId);
    }
}
