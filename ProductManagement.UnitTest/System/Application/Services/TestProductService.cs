using Moq;
using ProductManagement.Application.Product.Services;
using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;
using ProductManagement.UnitTest.System.Fixtures;

namespace ProductManagement.UnitTest.System.Application.Services
{
    public class TestProductService
    {
        [Fact]
        public async Task GetProductById_ExistsProduct()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            var result = await serviceProduct.GetByIdAsync(1);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetProductById_NoExists()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            try
            {
                await serviceProduct.GetByIdAsync(2);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Equal("No existe informacion relacionados con el producto", ex.Message);
            }

        }

        [Fact]
        public async Task CreateProduct_Sucess()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<Products>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            var result = await serviceProduct.CreateAsync(ProductFixtures.ProductRequestDtoTest);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateProduct_Sucess()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<int>(), It.IsAny<Products>()))
                .ReturnsAsync(ProductFixtures.ProductUpdateTest);
            mockRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductUpdateTest);
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            var result = await serviceProduct.UpdateAsync(1, ProductFixtures.ProductBadRequestDtoTest);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(ProductFixtures.ProductRequestDtoTest.Name, result.Name);
        }

        [Fact]
        public async Task UpdateProduct_NotFound()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
             .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()));
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            try
            {
                await serviceProduct.UpdateAsync(2, ProductFixtures.ProductBadRequestDtoTest);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.NotNull(ex);
                Assert.Equal("No existe informacion relacionados con el producto", ex.Message);
            }
        }

        [Fact]
        public async Task RemoveProduct_Sucess()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockRepository
                .Setup(repository => repository.RemoveAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            var serviceProduct = new ProductService(mockRepository.Object);
            //Act
            var result = await serviceProduct.RemoveAsync(1);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProduct_NoFound()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            mockRepository
                .Setup(repository => repository.RemoveAsync(It.IsAny<int>()))
                .ReturnsAsync(false);
            var serviceProduct = new ProductService(mockRepository.Object);
            try
            {
                await serviceProduct.RemoveAsync(1);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.NotNull(ex);
                Assert.Equal("No existe informacion relacionados con el producto", ex.Message);
            }
        }
    }
}
