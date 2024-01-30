using BillPayments_LookUp_Validation.Models.Responses;
using BillPayments_LookUp_Validation.Models.Requests;

namespace BillPayments_LookUp_Validation.Services
{
    public interface IStudentService
    {
        Task<AddStudentResponse> AddNewStudentAsync(AddStudentRequest request);
        Task<GetStudentByIdResponse> GetStudentByIdAsync(GetStudentByIdRequest request);
    }

}
