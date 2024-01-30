using BillPayments_LookUp_Validation.Models;

namespace BillPayments_LookUp_Validation.Services
{
    public interface IUDTM_LOV
    {
        Task<List<UDTM_LOV>> GetAllAsync();
    }
}
