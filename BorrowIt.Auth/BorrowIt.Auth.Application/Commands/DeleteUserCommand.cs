using System;
using BorrowIt.Common.Infrastructure.Abstraction.Commands;

namespace BorrowIt.Auth.Application.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}