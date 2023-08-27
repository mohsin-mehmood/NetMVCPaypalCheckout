using PaypalDemo.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaypalDemo.Paypal
{
    public class PaypalService
    {
        private readonly HttpClient _httpClient;

        public PaypalService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Paypal");
        }

        public async Task<T> CreateOrder<T>(CreateOrderModel model)
        {
            var requestUri = "v2/checkout/orders";

            var httpResponse = await _httpClient.PostAsJsonAsync<CreateOrderModel>(requestUri, model);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadFromJsonAsync<T>();

            return response;
        }


        public async Task<T> CaptureOrder<T>(string orderId)
        {
            var requestUri = $"/v2/checkout/orders/{orderId}/capture";

            var httpResponse = await _httpClient.PostAsync(requestUri, new StringContent(string.Empty, Encoding.UTF8, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadFromJsonAsync<T>();

            return response;
        }
    }
}
