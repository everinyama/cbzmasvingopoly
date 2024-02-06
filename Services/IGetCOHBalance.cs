using BillPayments_LookUp_Validation.Models;
using BillPayments_LookUp_Validation.Models.Requests;

namespace BillPayments_LookUp_Validation.Services
{
    public interface IGetCOHBalance
    {
        Task<AuthRequest> ReadIncomingEsbRequestAsync(BALANCE_ENQ_REQ request, string reference);
        Task<BALANCE_ENQ_RES> SendAuthRequestAsync(AuthRequest request);
        string SerializeToXml(object obj);
    }
}