using System.Xml.Serialization;

namespace BillPayments_LookUp_Validation.Models
{
    // MasvingoPolyCollegeSuccess myDeserializedClass = JsonConvert.DeserializeObject<MasvingoPolyCollegeSuccess>(myJsonResponse);
    public class MasvingoPolyCollegeSuccess
    {
        public string Name { get; set; }
        public string StudentNo { get; set; }
        public string Level { get; set; }
        public string Area { get; set; }
    }





}
