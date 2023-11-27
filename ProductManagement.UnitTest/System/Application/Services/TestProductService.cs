using Moq;
using ProductManagement.Application.Common.Exeptions;
using ProductManagement.Application.Product.Services;
using ProductManagement.Domain.Core;
using ProductManagement.Domain.ExternalServices;
using ProductManagement.Domain.ExternalServices.Discount;
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
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
            .Returns(ProductFixtures.StatusValues);
            mockRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
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
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
            .Returns(ProductFixtures.StatusValues);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await serviceProduct.GetByIdAsync(2));

        }

        [Fact]
        public async Task CreateProduct_Sucess()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockRepository
                .Setup(repository => repository.CreateAsync(It.IsAny<Products>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockclientApi
             .Setup(clienteApi => clienteApi.GetDataItemAsync(It.IsAny<int>()))
             .ReturnsAsync(new DiscountData { Id = 1, Discount = 10 });

            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
            .Returns(ProductFixtures.StatusValues);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
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
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<int>(), It.IsAny<Products>()))
                .ReturnsAsync(ProductFixtures.ProductUpdateTest);
            mockRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductUpdateTest);
            mockclientApi
             .Setup(clienteApi => clienteApi.GetDataItemAsync(It.IsAny<int>()))
             .ReturnsAsync(new DiscountData { Id = 1, Discount = 10 });
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
           .Returns(ProductFixtures.StatusValues);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
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
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockRepository
             .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()));
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
           .Returns(ProductFixtures.StatusValues);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await serviceProduct.UpdateAsync(2, ProductFixtures.ProductBadRequestDtoTest));
        }

        [Fact]
        public async Task RemoveProduct_Sucess()
        {
            //Arrage
            var mockRepository = new Mock<IProductRepository>();
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockRepository
                .Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockRepository
                .Setup(repository => repository.RemoveAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
            .Returns(ProductFixtures.StatusValues);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
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
            var mockclientApi = new Mock<IProductApiClient>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockRepository
                .Setup(repository => repository.RemoveAsync(It.IsAny<int>()))
                .ReturnsAsync(false);
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
                .Returns(ProductFixtures.StatusValues);
            var serviceProduct = new ProductService(mockRepository.Object, mockclientApi.Object, mockProductStatusCache.Object);
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await serviceProduct.RemoveAsync(1));
        }
    }
}
