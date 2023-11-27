namespace ProductManagement.Domain.Product
{
    /// <summary>
    /// Clase que representa un producto.
    /// </summary>
    public class Products
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
        public string StatusName { get; set; } = null!;

        /// <summary>
        /// Obtiene o establece la cantidad en stock del producto.
        /// </summary>
        public decimal Stock { get; set; } = 0;

        /// <summary>
        /// Obtiene o establece la descripción del producto.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el precio del producto.
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// Obtiene o establece el descuento aplicado al producto.
        /// </summary>
        public decimal Discount { get; set; } = 0;

        /// <summary>
        /// Obtiene o establece el precio final del producto después de aplicar el descuento.
        /// </summary>
        public decimal FinalPrice { get; set; } = 0;
        /// <summary>
        /// Obtiene o establece la fecha de registro del objeto.
        /// </summary>
        public DateTime DateRegistration { get; set; } = DateTime.Now;

        /// <summary>
        /// Obtiene o establece la fecha de la última actualización del objeto.
        /// </summary>
        public DateTime DateUpdate { get; set; } = DateTime.Now;
    }

}
