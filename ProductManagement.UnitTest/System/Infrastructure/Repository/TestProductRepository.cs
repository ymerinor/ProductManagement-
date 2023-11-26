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

        public void Dispose()
        {
            _fixture.Dispose();
        }
    }
}
