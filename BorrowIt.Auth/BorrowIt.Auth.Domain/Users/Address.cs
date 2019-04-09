using System;
using BorrowIt.Common.Extensions;

namespace BorrowIt.Auth.Domain.Users
{
    public class Address
    {
        public Address(string postalCode, string street, string city)
        {
            SetCity(city);
            SetStreet(street);
            SetPostalCode(postalCode);
        }

        public string City { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }

        public void UpdateAddress(string city, string street, string postalCode)
        {
            SetCity(city);
            SetStreet(street);
            SetPostalCode(postalCode);
        }

        private void SetCity(string city)
        {
            city.ValidateNullOrEmptyString(nameof(city));

            City = city;
        }
        
        private void SetStreet(string street)
        {
            street.ValidateNullOrEmptyString(nameof(street));

            Street = street;
        }
        
        private void SetPostalCode(string postalCode)
        {
            postalCode.ValidateNullOrEmptyString(nameof(postalCode));


            PostalCode = postalCode;
        }
    }
}