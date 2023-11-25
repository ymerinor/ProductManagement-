﻿namespace ProductManagement.Application.Product.Dto
{
    public class ProductsDto
    {
        /// <summary>
        /// Obtiene o establece el identificador del producto.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del producto.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece el estado del producto.
        /// </summary>
        public bool StatusName { get; set; }

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

        /// <summary>
        /// Obtiene o establece el descuento aplicado al producto.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Obtiene o establece el precio final del producto después de aplicar el descuento.
        /// </summary>
        public decimal FinalPrice { get; set; }
    }
}
