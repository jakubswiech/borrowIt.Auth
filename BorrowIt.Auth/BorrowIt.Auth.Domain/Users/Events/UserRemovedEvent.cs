using System;
using BorrowIt.Common.Rabbit.Abstractions;
using BorrowIt.Common.Rabbit.Attributes;

namespace BorrowIt.Auth.Domain.Users.Events
{
    [RabbitMessage("Auth")]
    public class UserRemovedEvent : IMessage
    {
        public UserRemovedEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}