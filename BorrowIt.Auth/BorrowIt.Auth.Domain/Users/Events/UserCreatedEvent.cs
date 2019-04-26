using System;
using System.Collections.Generic;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.Attributes;

namespace BorrowIt.Auth.Domain.Users.Events
{
    [RabbitMessage("Auth")]
    public class UserCreatedEvent : IMessage
    {
        public UserCreatedEvent(Guid id, string userName, string email, IEnumerable<string> roles, string firstName, string secondName, DateTime birthDate, DateTime? modifyDate, AddressEventData address)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Roles = roles;
            FirstName = firstName;
            SecondName = secondName;
            BirthDate = birthDate;
            ModifyDate = modifyDate;
            Address = address;
        }

        public Guid Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public string FirstName { get; }
        public string SecondName { get; }
        public DateTime BirthDate { get; }
        public DateTime? ModifyDate { get; }
        public AddressEventData Address { get; }
    }
}