using ProductManagement.Domain.Product;

namespace ProductManagement.Application.Product.Dto
{
    public class ProductsRequestDto
    {
        /// <summary>
        /// Obtiene o establece el nombre del producto.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece el estado del producto.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Obtiene o establece la cantidad en stock del producto.
        /// </summary>
        public decimal Stock { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción del producto.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtiene o establece el precio del producto.
        /// </summary>
        public decimal Price { get; set; }

        // <summary>
        /// Convierte de manera explícita un objeto ProductsRequestDto a un objeto Products.
        /// </summary>
        /// <param name="requestDto">Objeto ProductsRequestDto a convertir.</param>
        /// <returns>Un objeto Products creado a partir de la información en ProductsRequestDto.</returns>
        public static explicit operator Products(ProductsRequestDto requestDto)
        {
            return new Products
            {
                Name = requestDto.Name,
                Stock = requestDto.Stock,
                Description = requestDto.Description,
                Price = requestDto.Price,
                Discount = 0,
            };
        }
    }

}
