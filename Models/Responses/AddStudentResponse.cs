namespace BillPayments_LookUp_Validation.Models.Responses
{
    // Response Model
    public class AddStudentResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public object Student { get; set; }
        public object StudentsList { get; set; }
    }
}
