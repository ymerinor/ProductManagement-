using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Repository;
using ProductManagement.UnitTest.System.Fixtures;

namespace ProductManagement.UnitTest.System.Infrastructure.Repository
{
    public class TestProductRepository : IDisposable
    {
        private readonly SqliteInMemoryFixture _fixture;

        public TestProductRepository()
        {
            _fixture = new SqliteInMemoryFixture();
        }
        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnProduct()
        {
            using (var context = new ProductManagementDbContext(_fixture.CreateOptions<ProductManagementDbContext>()))
            {
                var repository = new ProductRepository(context);
                // Act
                var result = await repository.GetByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.ProductId);
            }
        }

        [Fact]
        public async Task GetByIdAsync_WithNotValidId_ShouldReturnProduct()
        {
            using (var context = new ProductManagementDbContext(_fixture.CreateOptions<ProductManagementDbContext>()))
            {
                // Arrange
                var repository = new ProductRepository(context);

                // Act
                var result = await repository.GetByIdAsync(2);

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task CreteProductAsync_Sucess()
        {
            using (var context = new ProductManagementDbContext(_fixture.CreateOptions<ProductManagementDbContext>()))
            {
                // Arrange
                var repository = new ProductRepository(context);
                // Act
                var result = await repository.CreateAsync(ProductFixtures.ProductCreateTest);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(ProductFixtures.ProductCreateTest.Name, result.Name);
            }
        }

        [Fact]
        public async Task Update_ProductAsync_Sucess()
        {
            using (var context = new ProductManagementDbContext(_fixture.CreateOptions<ProductManagementDbContext>()))
            {
                // Arrange
                var repository = new ProductRepository(context);
                // Act
                var result = await repository.UpdateAsync(1, ProductFixtures.ProductCreateTest);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(ProductFixtures.ProductCreateTest.Name, result.Name);
            }
        }

        [Fact]
        public async Task Update_NoProductAsync()
        {
            using (var context = new ProductManagementDbContext(_fixture.CreateOptions<ProductManagementDbContext>()))
            {
                // Arrange
                var repository = new ProductRepository(context);
                // Act
                try
                {
                    await repository.UpdateAsync(2, ProductFixtures.ProductCreateTest);
                }
                catch (Exception ex)
                {
                    // Assert
                    Assert.Equal("No existe informacion relacionada con el producto", ex.Message);
                }
            }
        }
        public void Dispose()
        {
            _fixture.Dispose();
        }
    }
}
