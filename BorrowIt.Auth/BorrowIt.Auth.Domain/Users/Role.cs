using System;

namespace BorrowIt.Auth.Domain.Users
{
    public class Role
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        
        public Role(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
    }
}