namespace BillPayments_LookUp_Validation.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> GetAccessTokenAsync(string email, string password);
    }

}
