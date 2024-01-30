namespace BillPayments_LookUp_Validation.Models
{
    public class tblPending
    {
        public decimal ID { get; set; }
        public string TransRef { get; set; }
        public string RegNumber { get; set; }
        public string FullName { get; set; }
        public string Bank { get; set; }
        public string PaymentCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string ExternalTransRef { get; set; }
        public string TransStatus { get; set; }
        public DateTime ProcessDate { get; set; }
    }
}
