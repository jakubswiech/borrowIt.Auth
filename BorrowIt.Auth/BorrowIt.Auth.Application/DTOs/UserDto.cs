using System;
using BorrowIt.Common.Infrastructure.Abstraction.DTOs;

namespace BorrowIt.Auth.Application.DTOs
{
    public class UserDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get;  set; }
        public DateTime CreateDate { get; }
        public DateTime BirthDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}