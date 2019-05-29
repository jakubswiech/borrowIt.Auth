using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.Auth.Application.Commands
{
    public class UpdateUserCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}