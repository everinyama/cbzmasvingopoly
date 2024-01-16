using System.Xml.Serialization;

namespace BillPayments_LookUp_Validation.Models
{
    [XmlRoot("BILL_VALIDATION")]
    public class BillValidation
    {
        [XmlElement("FIELD_NAME")]
        public string FieldName { get; set; }

        [XmlElement("FIELD_VALUE")]
        public string FieldValue { get; set; }
    }

	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(BILLVALIDATION));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (BILLVALIDATION)serializer.Deserialize(reader);
	// }

	[XmlRoot(ElementName = "STATUS")]
	public class STATUS
	{

		[XmlElement(ElementName = "VALID")]
		public string VALID { get; set; }

		[XmlElement(ElementName = "DESC")]
		public string DESC { get; set; }
	}

	[XmlRoot(ElementName = "BILL_VALIDATION")]
	public class BILLVALIDATION
	{

		[XmlElement(ElementName = "STATUS")]
		public STATUS STATUS { get; set; }

		[XmlElement(ElementName = "FIELD_VALUE")]
		public string FIELDVALUE { get; set; }

		[XmlElement(ElementName = "FIELD_DESCRIPTION")]
		public string FIELDDESCRIPTION { get; set; }
	}
}
