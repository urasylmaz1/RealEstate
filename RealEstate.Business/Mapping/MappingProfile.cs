using System;
using AutoMapper;
using RealEstate.Business.DTOs;
using RealEstate.Entity.Concrete;

namespace RealEstate.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region AppUser
        CreateMap<AppUser, AppUserDto>();
        #endregion

        #region Property
        CreateMap<Property, PropertyDto>()
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime)
            )
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime)
            )
            .ForMember(
                dest => dest.PropertyType,
                opt => opt.MapFrom(src => src.PropertyType)
            );
        CreateMap<PropertyCreateDto, Property>();
        CreateMap<PropertyUpdateDto, Property>();
        #endregion

        #region PropertyType
        CreateMap<PropertyType, PropertyTypeDto>()
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime)
            )
            .ForMember(
                dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => src.UpdatedAt.UtcDateTime)
            );

        CreateMap<PropertyTypeCreateDto, PropertyType>();
        CreateMap<PropertyTypeUpdateDto, PropertyType>();
        #endregion

        #region PropertyImage
        CreateMap<PropertyImage, PropertyImageDto>();
        CreateMap<PropertyImageDto, PropertyImage>();
        #endregion

        #region Inquiry
        CreateMap<Inquiry, InquiryDto>()
            .ForMember(dest => dest.PropertyId, opt => opt.MapFrom(src => src.PropertyId));

        CreateMap<InquiryCreateDto, Inquiry>();
        CreateMap<InquiryUpdateDto, Inquiry>();
        #endregion
    }
}
