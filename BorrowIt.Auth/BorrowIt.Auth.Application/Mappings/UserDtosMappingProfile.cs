using AutoMapper;
using BorrowIt.Auth.Application.DTOs;
using BorrowIt.Auth.Domain.Users;

namespace BorrowIt.Auth.Application.Mappings
{
    public class UserDtosMappingProfile : Profile
    {
        public UserDtosMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.Address.City))
                .ForMember(x => x.Street, opt => opt.MapFrom(x => x.Address.Street))
                .ForMember(x => x.PostalCode, opt => opt.MapFrom(x => x.Address.PostalCode));
        }
    }
}