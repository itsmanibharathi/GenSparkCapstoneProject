using api.Models;
using api.Models.Dtos.UserPropertyInteractionDto;
using api.Models.Dtos.PropertSeedData;
using api.Models.Dtos.PropertyAmenityDtos;
using api.Models.Dtos.PropertyDtos;
using api.Models.Dtos.PropertyMediaFile;
using api.Models.Dtos.SubscriptionDto;
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

            CreateMap<CreatePropertyDto, Property>();
            CreateMap<Property, ReturnPropertyDto>();

            CreateMap<EditPropertyHomeDto, PropertyHome>();
            CreateMap<PropertyHome, ReturnPropertyHomeDto>();

            CreateMap<EditPropertyLandDto, PropertyLand>();
            CreateMap<PropertyLand, ReturnPropertyLandDto>();

            CreateMap<EditPropertyAmenityDto, PropertyAmenity>();
            CreateMap<PropertyAmenity, ReturnPropertyAmenityDto>();
            
            CreateMap<PropertyMediaFile, ReturnPropertyMediaFileDto>();
            CreateMap<EditPropertyMediaFileDto, PropertyMediaFile>();
            
            CreateMap<Property, ReturnPropertyDto>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.MediaFiles, opt => opt.MapFrom(src => src.MediaFiles));

            CreateMap<EditPropertyDto, Property>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.MediaFiles, opt => opt.MapFrom(src => src.MediaFiles));

            CreateMap<User, BuyerViewOwnerInfoDto>();

            CreateMap<SubscriptionPlan, ReturnSubscriptionPlanDto>();
        }
    }
}
