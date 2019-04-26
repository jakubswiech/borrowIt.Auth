namespace BorrowIt.Auth.Domain.Users.Events
{
    public class AddressEventData
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}