using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Auth.Application.Services;
using BorrowIt.Auth.Domain.Users.DataStructure;
using BorrowIt.Common.Infrastructure.Abstraction;

namespace BorrowIt.Auth.Application.Handlers.Commands
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        public async Task HandleAsync(UpdateUserCommand command)
        {
            var dataStructure = _mapper.Map<UserDataStructure>(command);
            await _usersService.UpdateUserAsync(dataStructure);
        }
    }
}