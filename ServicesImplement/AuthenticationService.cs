using BillPayments_LookUp_Validation.Models.CST;
using BillPayments_LookUp_Validation.Services.Authentication;
using System.Net.Http.Json;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _config;

        public AuthenticationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string?> GetAccessTokenAsync(string email, string password)
        {
            var request = new AuthenticationRequest { Email = email, Password = password };
            var csturl = _config["CstAuthUrl"];
            var response = await _httpClient.PostAsJsonAsync(/*_config["CstAuthUrl"]*/csturl, request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
                return result?.AccessToken;
            }
            else
            {
                // Handle error response
                throw new InvalidOperationException("Failed to authenticate");
            }
        }
    }

}
