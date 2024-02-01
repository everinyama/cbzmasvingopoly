using BillPayments_LookUp_Validation.Models.CST;

namespace BillPayments_LookUp_Validation.Services
{
    public interface IWalletAccountService
    {
        Task<AccountDetailsResponse> GetAccountDetailsAsync(string token, string identifier);
    }

}
