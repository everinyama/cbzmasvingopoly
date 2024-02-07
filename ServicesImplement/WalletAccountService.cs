using BillPayments_LookUp_Validation.Models;
using BillPayments_LookUp_Validation.Models.CST;
using BillPayments_LookUp_Validation.Services;
using System.Net.Http.Headers;

namespace BillPayments_LookUp_Validation.ServicesImplement
{
    public class WalletAccountService : IWalletAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WalletAccountService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<AccountDetailsResponse> GetAccountDetailsAsync(string token, string identifier)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var cstLookUpUrl = _configuration["CstLookUpUrl"] + identifier + "/details";
            var response = await _httpClient.GetAsync(cstLookUpUrl);

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
