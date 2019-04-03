using System;
using System.Collections.Generic;
using BorrowIt.Common.Domain;

namespace BorrowIt.Auth.Domain.Users
{
    public class User : DomainModel
    {

        public string UserName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public IEnumerable<Role> Roles { get; protected set; }
        public string FirstName { get; protected set; }
        public string SecondName { get; protected set; }
        

        public User(IEnumerable<Role> roles, string password, string email, string userName, string firstName, string secondName)
        {
            Roles = roles;
            Password = password;
            Email = email;
            UserName = userName;
            FirstName = firstName;
            SecondName = secondName;
        }

        public void UpdateUser()
        {
            
        }
    }

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
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            City = city;
        }
        
        private void SetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentNullException(nameof(street));
            }

            Street = street;
        }
        
        private void SetPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentNullException(nameof(postalCode));
            }

            PostalCode = postalCode;
        }
    }
}