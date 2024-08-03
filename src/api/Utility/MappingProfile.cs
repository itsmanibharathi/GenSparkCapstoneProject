using api.Models;
using api.Models.Dtos.UserPropertyInteractionDto;
using api.Models.Dtos.PropertSeedData;
using api.Models.Dtos.PropertyAmenityDtos;
using api.Models.Dtos.PropertyDtos;
using api.Models.Dtos.PropertyMediaFile;
using api.Models.Dtos.SubscriptionDto;
using api.Models.Dtos.UserDtos;
using api.Models.Dtos.UserPropertyInteractionDto;
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

            CreateMap<EditPropertyHomeDto, PropertyHome>();
            CreateMap<PropertyHome, ReturnPropertyHomeDto>()
                .PreserveReferences();
            CreateMap<PropertyLand, ReturnPropertyLandDto>()
                .PreserveReferences();

            CreateMap<EditPropertyAmenityDto, PropertyAmenity>();
            CreateMap<EditPropertyLandDto, PropertyLand>();

            CreateMap<PropertyAmenity, ReturnPropertyAmenityDto>()
                .PreserveReferences();

            CreateMap<Property, ReturnUserPropertyInteractionDto>();

            CreateMap<Property, ReturnPropertyDto>()
                .PreserveReferences()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.MediaFiles, opt => opt.MapFrom(src => src.MediaFiles));
      



            CreateMap<EditPropertyDto,Property>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.MediaFiles, opt => opt.MapFrom(src => src.MediaFiles));

            CreateMap<SeedPropertyDto,Property>()
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.MediaFiles, opt => opt.MapFrom(src => src.MediaFiles));

            CreateMap<SeedPropertyHomeDto, PropertyHome>();
            CreateMap<SeedPropertyAmenityDto, PropertyAmenity>();
            CreateMap<SeedPropertyMediaFileDto, PropertyMediaFile>();
            CreateMap<SeedPropertyLandDto, PropertyLand>();

            CreateMap<User, BuyerViewOwnerInfoDto>();


            CreateMap<EditPropertyMediaFileDto, PropertyMediaFile>();
                                
            CreateMap<PropertyMediaFile, ReturnPropertyMediaFileDto>()
                                .PreserveReferences();


            CreateMap<SubscriptionPlan, ReturnSubscriptionPlanDto>();
        }
    }
}
