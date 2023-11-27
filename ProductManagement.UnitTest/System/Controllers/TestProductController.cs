using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.API.Controllers;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.Core;
using ProductManagement.UnitTest.System.Fixtures;

namespace ProductManagement.UnitTest.System.Controllers
{
    public class TestProductController
    {
        [Fact]
        public async Task Get_Succes_StatusCode200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductDtoTest);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);
            //Act
            var result = (OkObjectResult)await controller.Get(1);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_Sucess_InvokeServiceOnce()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductDtoTest);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Get(1);
            //Assert
            mockProductServices.Verify(
                service => service.GetByIdAsync(It.IsAny<int>()), Times.Once());
        }


        [Fact]
        public async Task Get_Sucess_ReturnInformationProduct()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductDtoTest);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Get(1);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<ProductsDto>();
        }


        [Fact]
        public async Task Get_ContentProduct_Return204()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Get(2);
            //Assert
            result.Should().BeOfType<NoContentResult>();
            var objectResult = (NoContentResult)result;
            objectResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Post_Succes_StatusCode200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
             .Returns(ProductFixtures.StatusValues);
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);
            //Act
            var result = (OkObjectResult)await controller.Post(ProductFixtures.ProductRequestDtoTest);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Post_Sucess_InvokeServiceOnce()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
            .Returns(ProductFixtures.StatusValues);
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            await controller.Post(ProductFixtures.ProductRequestDtoTest);
            //Assert
            mockProductServices.Verify(
                service => service.CreateAsync(It.IsAny<ProductsRequestDto>()), Times.Once());
        }


        [Fact]
        public async Task Post_BadRequest_Return400()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
                .Returns(ProductFixtures.StatusValues);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Post(ProductFixtures.ProductBadRequestDtoTest);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = (BadRequestObjectResult)result;
            objectResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Post_RequestOK_Return200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
             .Returns(ProductFixtures.StatusValues);
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Post(ProductFixtures.ProductRequestDtoTest);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Put_Succes_StatusCode200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
                .Returns(ProductFixtures.StatusValues);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);
            //Act
            var result = (OkObjectResult)await controller.Put(1, ProductFixtures.ProductRequestDtoTest);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Put_Sucess_InvokeServiceOnce()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
                .Returns(ProductFixtures.StatusValues);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Put(1, ProductFixtures.ProductRequestDtoTest);
            //Assert
            mockProductServices.Verify(
                service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductsRequestDto>()), Times.Once());
        }

        [Fact]
        public async Task Put_BadRequest_Return400()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            mockProductStatusCache.Setup(cache => cache.GetProductStatus())
                .Returns(ProductFixtures.StatusValues);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Put(1, ProductFixtures.ProductBadRequestDtoTest);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var objectResult = (BadRequestObjectResult)result;
            objectResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task Delete_Succes_StatusCode200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.RemoveAsync(It.IsAny<int>())).ReturnsAsync(true);
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);
            //Act
            var result = (OkObjectResult)await controller.Delete(1);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Delete_Sucess_InvokeServiceOnce()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var mockProductStatusCache = new Mock<IProductStatusCache>();
            mockProductServices
                .Setup(service => service.RemoveAsync(It.IsAny<int>()));
            var controller = new ProductController(mockProductServices.Object, mockProductStatusCache.Object);

            //Act
            var result = await controller.Delete(1);
            //Assert
            mockProductServices.Verify(
                service => service.RemoveAsync(It.IsAny<int>()), Times.Once());
        }
    }
}