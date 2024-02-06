using BillPayments_LookUp_Validation.Models.CST;
using BillPayments_LookUp_Validation.Services;
using System.Net.Http.Headers;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class WalletAccountService : IWalletAccountService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _config;

        public WalletAccountService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<AccountDetailsResponse> GetAccountDetailsAsync(string token, string identifier)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var csTAccountLookUp = _config["CstLookUpUrl"];
            //var response = await _httpClient.GetAsync($"http://192.168.0.82:8084/api/v1/client-services/account/{identifier}/details");
            var response = await _httpClient.GetAsync(csTAccountLookUp + identifier + "/details");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AccountDetailsResponse>();
            }
            else
            {
                // Handle error response
                throw new InvalidOperationException("Failed to retrieve account details");
            }
        }
    }

}
