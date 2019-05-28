using System.Threading.Tasks;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Auth.Application.Services;
using BorrowIt.Common.Infrastructure.Abstraction;

namespace BorrowIt.Auth.Application.Handlers.Commands
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUsersService _usersService;

        public DeleteUserCommandHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task HandleAsync(DeleteUserCommand command)
        {
            await _usersService.RemoveUserAsync(command.Id);
        }
    }
}