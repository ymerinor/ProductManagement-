using ProductManagement.Domain.ExternalServices.Discount;

namespace ProductManagement.Domain.ExternalServices
{
    /// <summary>
    /// Interfaz para el cliente API que obtiene datos de descuento para un producto.
    /// </summary>
    public interface IProductApiClient
    {
        /// <summary>
        /// Obtiene datos de descuento para un producto específico.
        /// </summary>
        /// <param name="productId">Identificador único del producto.</param>
        /// <returns>Los datos de descuento asociados al producto.</returns>
        Task<DiscountData> GetDataItemAsync(int productId);
    }
}
