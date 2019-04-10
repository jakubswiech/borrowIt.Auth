using AutoMapper;
using BorrowIt.Auth.Domain.Users;
using BorrowIt.Auth.Infrastructure.Entities.Users;

namespace BorrowIt.Auth.Infrastructure.Mappings.Users
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Roles));

            CreateMap<Role, RoleEntity>();

        }
    }
}