using System.Xml.Serialization;

namespace BillPayments_LookUp_Validation.Models
{
    public class BALANCE_ENQ_RES
    {
        public STATUS STATUS { get; set; }
        public DETAILS? DETAILS { get; set; }
    }
    public class DETAILS
    {
        public string? ACCOUNT_NAME { get; set; }
        public string? ACCOUNT_BALANCE { get; set; }
    }
}
