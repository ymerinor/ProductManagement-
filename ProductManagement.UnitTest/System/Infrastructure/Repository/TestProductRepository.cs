using Microsoft.EntityFrameworkCore;
using Moq;
using ProductManagement.Domain.Product;
using ProductManagement.Infrastructure;
using ProductManagement.Infrastructure.Repository;
using ProductManagement.UnitTest.System.Fixtures;

namespace ProductManagement.UnitTest.System.Infrastructure.Repository
{
    public class TestProductRepository : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;

        public TestProductRepository(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task GetByIdAsync_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var options = _fixture.CreateOptions<ProductManagementDbContext>();

            // Configurar el DbSet mock
            var mockDbSet = new Mock<DbSet<Products>>();
            mockDbSet.Setup(d => d.FindAsync(It.IsAny<int>())).ReturnsAsync(new Products { ProductId = 1, Name = "Product 1" });

            // Configurar el DbContext utilizando el DbSet mock.
            var dbContext = new Mock<ProductManagementDbContext>(options);
            dbContext.Setup(d => d.Set<Products>()).Returns(mockDbSet.Object);

            var repository = new ProductRepository(dbContext.Object);

            // Act
            var result = await repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal("Product 1", result.Name);
        }
    }
}
