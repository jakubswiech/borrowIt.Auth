using AutoMapper;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Auth.Domain.Users.DataStructure;

namespace BorrowIt.Auth.Application.Mappings
{
    public class UserCommandsMappingProfile : Profile
    {
        public UserCommandsMappingProfile()
        {

            CreateMap<CreateUserCommand, UserDataStructure>()
                .ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}