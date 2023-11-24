using Moq;
using ProductManagement.Application.Product.Services;
using ProductManagement.Domain.Repository.Interface;
using ProductManagement.UnitTest.System.Fixtures;

namespace ProductManagement.UnitTest.System.Application.Services
{
    public class TestProductService
    {
        [Fact]
        public async Task GetUserById_ExistsProduct()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            var result = await serviceProduct.GetByIdAsync(1);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetUserById_NoExists()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            var result = await serviceProduct.GetByIdAsync(2);
            //Assert
            Assert.Null(result);
        }
    }
}
