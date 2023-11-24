using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Controllers;
using ProductManagement.Domain.Product;

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
                .ReturnsAsync(new List<Products>() {
                    new() {
                        ProductId = 1,
                        Description = "Prodcut",
                        Discount = 1,
                        FinalPrice = 1,
                        Name = "Name",
                        Price = 1,
                        StatusName = true,
                        Stock = 1
                    }
                });
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
                .ReturnsAsync(new List<Products>());
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
                .ReturnsAsync(new List<Products>() {
                    new() {
                        ProductId = 1,
                        Description = "Prodcut",
                        Discount = 1,
                        FinalPrice = 1,
                        Name = "Name",
                        Price = 1,
                        StatusName = true,
                        Stock = 1
                    }
                });
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Get(1);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<Products>>();
        }


        [Fact]
        public async Task Get_NoUserFound_Return404()
        {
            //Arrage
            var mockProductServices = new Mock<IProductService>();
            mockProductServices
                .Setup(service => service.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Products>());
            var controller = new ProductController(mockProductServices.Object);

            //Act
            var result = await controller.Get(1);
            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}