using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Auth.Application.DTOs;
using BorrowIt.Auth.Application.Queries;
using BorrowIt.Auth.Infrastructure.Repositories.Users;
using BorrowIt.Common.Infrastructure.Abstraction;

namespace BorrowIt.Auth.Application.Handlers.Queries
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> HandleAsync(GetUserQuery query)
        {
            var user = await _usersRepository.GetAsync(query.Id);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}