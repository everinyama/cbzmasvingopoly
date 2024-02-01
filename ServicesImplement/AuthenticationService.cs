using BillPayments_LookUp_Validation.Models.CST;
using BillPayments_LookUp_Validation.Services.Authentication;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetAccessTokenAsync(string email, string password)
        {
            var request = new AuthenticationRequest { Email = email, Password = password };

            var response = await _httpClient.PostAsJsonAsync("http://192.168.0.82:8086/api/v1/auth/authenticate", request);

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
