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
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Roles))
                .ForMember(x => x.AddressStreet, opt => opt.MapFrom(x => x.Address.Street))
                .ForMember(x => x.AddressCity, opt => opt.MapFrom(x => x.Address.City))
                .ForMember(x => x.AddressPostCode, opt => opt.MapFrom(x => x.Address.PostalCode));

            CreateMap<UserEntity, User>()
                .ForMember(x => x.Address,
                    opt => opt.MapFrom(x => new Address(x.AddressPostCode, x.AddressStreet, x.AddressCity)))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.Roles));

            CreateMap<Role, RoleEntity>();

        }
    }
}