using System.Collections.Generic;
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
        public IEnumerable<Role> Roles { get; protected set; }
        public string FirstName { get; protected set; }
        public string SecondName { get; protected set; }
        

        public User(IEnumerable<Role> roles, string email, string userName, string firstName, string secondName)
        {
            Roles = roles;
            SetUserName(userName);
            SetFirstName(firstName);
            SetSecondName(secondName);
        }

        public void UpdateUser(IEnumerable<Role> roles, string email, string userName, string firstName, string secondName)
        {
            SetUserName(userName);
            SetFirstName(firstName);
            SetSecondName(secondName);
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
    }
}