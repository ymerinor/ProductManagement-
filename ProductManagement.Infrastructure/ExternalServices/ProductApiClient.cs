using ProductManagement.Domain.ExternalServices;
using ProductManagement.Domain.ExternalServices.Discount;
using System.Net.Http.Formatting;

namespace ProductManagement.Infrastructure.ExternalServices
{
    /// </<inheritdoc/>
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// </<inheritdoc/>
        public async Task<DiscountData> GetDataItemAsync(int productId)
        {
            var httpClient = _httpClientFactory.CreateClient("ApiDataApiClient");
            var response = await httpClient.GetAsync($"{productId}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new DiscountData();
            }
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var dataItem = await response.Content.ReadAsAsync<DiscountData>(new[] { new JsonMediaTypeFormatter() });
            return dataItem;
        }
    }
}
