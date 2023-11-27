using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Domain.Core;

namespace ProductManagement.Infrastructure.Core
{
    /// <summary>
    /// Implementación de la interfaz IProductStatusCache utilizando IMemoryCache para gestionar la caché.
    /// </summary>
    public class MemoryCacheAdapter : IProductStatusCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Constructor de la clase MemoryCacheAdapter.
        /// </summary>
        /// <param name="memoryCache">Instancia de IMemoryCache para gestionar la caché en memoria.</param>
        public MemoryCacheAdapter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Obtiene los estados del producto desde la caché o actualiza la caché si es necesario.
        /// </summary>
        /// <returns>Un diccionario que representa los estados del producto.</returns>
        public Dictionary<int, string> SetProductStatus()
        {
            if (!_memoryCache.TryGetValue("ProductStatus", out Dictionary<int, string> cachedStatus))
            {
                // Si no hay datos en caché, actualizar la caché
                cachedStatus = UpdateCache();
            }

            return new Dictionary<int, string>(cachedStatus);
        }

        /// <summary>
        /// Actualiza la caché con los estados del producto.
        /// </summary>
        /// <returns>Un diccionario que representa los estados del producto.</returns>
        private Dictionary<int, string> UpdateCache()
        {
            // Lógica para obtener y actualizar los estados del producto desde la fuente de datos
            // En este ejemplo, simplemente hardcodeamos los valores
            var productStatus = new Dictionary<int, string>
        {
            { 1, "Active" },
            { 0, "Inactive" }
        };

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheDuration
            };

            // Agregar los datos a la caché con opciones de expiración
            _memoryCache.Set("ProductStatus", productStatus, cacheEntryOptions);

            return productStatus;
        }
    }

}
