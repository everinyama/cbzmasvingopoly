using BillPayments_LookUp_Validation.Models;

namespace BillPayments_LookUp_Validation.Services
{
    public interface ICSTAccountNumberValidation
    {
        string validate_cst_account_number(BillValidation billerVallidation);
    }
}

