namespace BillPayments_LookUp_Validation.Models.Responses
{
    // Response Model
    public class GetStudentByIdResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public Student Student { get; set; }
        public object StudentsList { get; set; }
    }

}
