using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Auth.Application.Services;
using BorrowIt.Auth.Domain.Users.DataStructure;
using BorrowIt.Common.Infrastructure.Abstraction;

namespace BorrowIt.Auth.Application.Handlers.Commands
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var dataStructure = _mapper.Map<UserDataStructure>(command);

            await _usersService.AddUserAsync(dataStructure);
        }
    }
}