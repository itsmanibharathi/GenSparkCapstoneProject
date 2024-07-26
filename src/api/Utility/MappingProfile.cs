using api.Models;
using api.Models.Dtos.UserDtos;
using AutoMapper;

namespace api.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserLoginDto, UserAuth>();
            CreateMap<UserAuth, ReturnUserLoginDto>();

            CreateMap<GetUserRegisterDto, User>();
            CreateMap<User, ReturnUserRegisterDto>();

            CreateMap<GetUserEditDto, User>();
            CreateMap<User, ReturnUserDto>();
        }
    }
}
