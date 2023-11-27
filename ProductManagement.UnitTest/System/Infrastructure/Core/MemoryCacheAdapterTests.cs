using Microsoft.Extensions.Caching.Memory;
using ProductManagement.Infrastructure.Core;
namespace ProductManagement.UnitTest.System.Infrastructure.Core
{
    public class MemoryCacheAdapterTests
    {
        [Fact]
        public void GetProductStatus_ReturnsCachedDataWithinTimeLimit()
        {
            // Arrange
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var cache = new MemoryCacheAdapter(memoryCache);

            // Act
            var result1 = cache.GetProductStatus();
            var result2 = cache.GetProductStatus();
            Assert.Equal(result1, result2);
        }
    }
}
