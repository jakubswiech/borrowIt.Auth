using System;
using System.Collections.Generic;

namespace BorrowIt.Auth.Domain.Users.DataStructure
{
    public class UserDataStructure
    {
        public Guid? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}