using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.API.Controllers;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.Product;
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
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);
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
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);

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
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Get(1);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Products>();
        }


        [Fact]
        public async Task Get_NoUserFound_Return404()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Get(2);
            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Post_Succes_StatusCode200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);
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
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Post(ProductFixtures.ProductRequestDtoTest);
            //Assert
            mockProductServices.Verify(
                service => service.CreateAsync(It.IsAny<ProductsRequestDto>()), Times.Once());
        }


        [Fact]
        public async Task Post_BadRequest_Return400()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Post(ProductFixtures.ProductBadRequestDtoTest);
            //Assert
            result.Should().BeOfType<BadRequestResult>();
            var objectResult = (BadRequestResult)result;
            objectResult.StatusCode.Should().Be(400);
        }


        [Fact]
        public async Task Post_RequestOK_Return200()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            mockProductServices
                .Setup(service => service.CreateAsync(It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);

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
            mockProductServices
                .Setup(service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);
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
            mockProductServices
                .Setup(service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductsRequestDto>()))
                .ReturnsAsync(ProductFixtures.ProductTest);
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Put(1, ProductFixtures.ProductRequestDtoTest);
            //Assert
            mockProductServices.Verify(
                service => service.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductsRequestDto>()), Times.Once());
        }

    }
}