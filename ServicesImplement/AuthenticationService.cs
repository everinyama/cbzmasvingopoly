using BillPayments_LookUp_Validation.Models.CST;
using BillPayments_LookUp_Validation.Services.Authentication;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthenticationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string?> GetAccessTokenAsync(string email, string password)
        {
            var request = new AuthenticationRequest { Email = email, Password = password };
            var cstAuthUrl = _configuration["CstAuthUrl"];

            var response = await _httpClient.PostAsJsonAsync(cstAuthUrl, request);

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
