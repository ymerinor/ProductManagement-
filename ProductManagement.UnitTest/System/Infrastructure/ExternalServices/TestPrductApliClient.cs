using Moq;
using Moq.Protected;
using ProductManagement.Infrastructure.ExternalServices;
using System.Net;

namespace ProductManagement.UnitTest.System.Infrastructure.ExternalServices
{
    public class TestPrductApliClient
    {
        [Fact]
        public async Task GetDataItemAsync_ShouldReturnDataItem()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"id\": 1, \"discount\": 10}")
                });

            var httpClient = new HttpClient(handlerMock.Object);
            // Configura la BaseAddress para que la URL sea relativa al ID proporcionado
            httpClient.BaseAddress = new Uri("https://6563e225ceac41c0761d2b8c.mockapi.io/id/");
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var productApiClient = new ProductApiClient(httpClientFactoryMock.Object);

            // Act
            var result = await productApiClient.GetDataItemAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(10, result.Discount);
        }


        [Fact]
        public async Task GetDataItemAsync_NotFoundResponse()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                });

            var httpClient = new HttpClient(handlerMock.Object);
            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            httpClient.BaseAddress = new Uri("https://6563e225ceac41c0761d2b8c.mockapi.io/id/");
            var apiDataApiClient = new ProductApiClient(httpClientFactoryMock.Object);

            // Act
            var result = await apiDataApiClient.GetDataItemAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
        }

    }
}
