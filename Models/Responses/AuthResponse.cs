namespace BillPayments_LookUp_Validation.Models.Requests
{
    public class AuthResponse
    {
        public string participant { get; set; }
        public string participantReference { get; set; }
        public string provider { get; set; }
        public string providerProduct { get; set; }
        public bool success { get; set; }
        public string accountName { get; set; }
        public string accountBalance { get; set; }
        public string providerResponse { get; set; }
        public string providerDescription { get; set; }
        public string providerReference { get; set; }
        public string receiptData { get; set; }
        public string aditionalBillData { get; set; }


    }
}
