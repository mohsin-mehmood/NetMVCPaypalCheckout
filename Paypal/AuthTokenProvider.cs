using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PaypalDemo.Paypal
{
    public interface IAuthTokenProvider
    {
        Task<string> GetAccessToken();
        Task RefreshToken();
    }

    public class AuthTokenProvider : IAuthTokenProvider
    {
        private readonly HttpClient _httpClient;
        private readonly PaypalConfig _paypalConfig;
        private string _token;

        public AuthTokenProvider(PaypalConfig paypalConfig)
        {

            _paypalConfig = paypalConfig;

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_paypalConfig.ApiUrl)
            };
        }

        public async Task<string> GetAccessToken()
        {
            if (_token == null)
                await RefreshToken();

            return _token;
        }

        public async Task RefreshToken()
        {
            var requestUri = "v1/oauth2/token";

            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            var auth = Base64Encode($"{_paypalConfig.ClientID}:{_paypalConfig.SecretKey}");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic ${auth}");

            var httpResponse = await _httpClient.PostAsync(requestUri, new FormUrlEncodedContent(parameters));

            httpResponse.EnsureSuccessStatusCode();

            var authResponse = await httpResponse.Content.ReadFromJsonAsync<AuthResponseModel>();
            _token = authResponse.AccessToken;

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
