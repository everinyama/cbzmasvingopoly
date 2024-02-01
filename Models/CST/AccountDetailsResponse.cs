namespace BillPayments_LookUp_Validation.Models.CST
{
    public class AccountDetailsResponse
    {
        public string Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public AddressDetails Address { get; set; }

        public List<string> Products { get; set; }
        public string ContactNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CreatedBy { get; set; }
        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }

        // Add any other necessary properties here
    }

    public class AddressDetails
    {
        public string UnitNumber { get; set; }
        public string StreetName { get; set; }
        public string Town { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }

}
