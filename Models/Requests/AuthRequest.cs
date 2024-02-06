namespace BillPayments_LookUp_Validation.Models.Requests
{
    public class AuthRequest
    {
        public string type { get; set; }
        public string participant { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string provider { get; set; }
        public string? participantReference { get; set; }
        public string billAccount { get; set; }
    }
}
