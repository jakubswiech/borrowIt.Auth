using System;
using System.Collections.Generic;
using BorrowIt.Auth.Domain.Users;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.Auth.Application.Commands
{
    public class CreateUserCommand : ICommand
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}