using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BorrowIt.Auth.Domain.Users.Helpers;
using BorrowIt.Auth.Domain.Users.Policies;
using BorrowIt.Common.Domain;
using BorrowIt.Common.Extensions;

namespace BorrowIt.Auth.Domain.Users
{
    public class User : DomainModel
    {

        public string UserName { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }
        public IEnumerable<Guid> Roles { get; protected set; }
        public string FirstName { get; protected set; }
        public string SecondName { get; protected set; }
        public DateTime CreateDate { get; }
        public DateTime BirthDate { get; protected set; }
        public DateTime? ModifyDate { get; protected set; }
        public Address Address { get; protected set; }
        

        public User(Guid id, IEnumerable<Guid> roles, string email, string userName, string firstName, string secondName, DateTime birthDate, Address address)
        {
            Id = id;
            SetBirthDate(birthDate);
            SetUserName(userName);
            SetFirstName(firstName);
            SetSecondName(secondName);
            SetEmail(email);
            Roles = roles.ToList();
            Address = address;
            CreateDate = DateTime.UtcNow;
            ModifyDate = null;
        }

        public void UpdateUser(string email, string userName, string firstName, string secondName, DateTime birthDate, IEnumerable<Guid> roles, Address address)
        {
            SetBirthDate(birthDate);
            SetUserName(userName);
            SetFirstName(firstName);
            SetSecondName(secondName);
            Roles = roles;
            Address = address;
            ModifyDate = DateTime.UtcNow;
        }
        
        private User()
        {
            
        }


        private void SetUserName(string userName)
        {
            userName.ValidateNullOrEmptyString(nameof(userName));
            UserName = userName;
        }
        private void SetFirstName(string firstName)
        {
            firstName.ValidateNullOrEmptyString(nameof(firstName));
            FirstName = firstName;
        }
        private void SetSecondName(string secondName)
        {
            secondName.ValidateNullOrEmptyString(nameof(secondName));
            SecondName = secondName;
        }

        public void SetPassword(string password)
        {
            password.Validate<MinimalEightLettersPasswordPolicy>();
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }
        private void SetBirthDate(DateTime birthDate)
        {
            if (birthDate > DateTime.UtcNow)
            {
                throw new Exception();
            }

            BirthDate = birthDate;
        }
        
        private void SetEmail(string email)
        {
            email.Validate<EmailPolicy>();

            Email = email;
        }

    }
}