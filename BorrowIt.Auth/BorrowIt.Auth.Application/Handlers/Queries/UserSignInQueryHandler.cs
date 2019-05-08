using System.Threading.Tasks;
using BorrowIt.Auth.Application.DTOs;
using BorrowIt.Auth.Application.Queries;
using BorrowIt.Auth.Application.Services;
using BorrowIt.Auth.Domain.Users.DataStructure;
using BorrowIt.Common.Infrastructure.Abstraction;

namespace BorrowIt.Auth.Application.Handlers.Queries
{
    public class UserSignInQueryHandler : IQueryHandler<SignInQuery,UserSignedInDto>
    {
        private readonly IUsersService _usersService;

        public UserSignInQuery(IUsersService usersService)
        {
            _usersService = usersService;
        }
        public async Task<UserSignedInDto> HandleAsync(SignInQuery query)
        {
            var dataStructure = new UserDataStructure() {UserName = query.UserName, Password = query.Password};
            var userDto = new UserSignedInDto() {Token = await _usersService.SignInAsync(dataStructure)};

            return userDto;
        }
    }
}