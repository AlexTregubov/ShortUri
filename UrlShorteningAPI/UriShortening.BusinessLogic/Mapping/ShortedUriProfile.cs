namespace UriShortening.BusinessLogic.Mapping
{
    using System;
    using Models;
    using Persistence.Context.Models;
    using AutoMapper;
    using Helpers;

    public class ShortedUriProfile : Profile
    {
        public ShortedUriProfile()
        {
            CreateMap<AddUriModel, ShortedUrl>()
                .ForMember(dest => dest.CreatedById, m => m.MapFrom(src => src.CreatedById))
                .ForMember(dest => dest.SourceUri, m => m.MapFrom(src => src.Uri))
                .ForMember(dest => dest.ShortUri, m => m.MapFrom(src => ShortUriGenerator.CreateShortUriKey() ))
                .ForMember(dest => dest.CreatedAt, m => m.MapFrom(src => DateTime.UtcNow));

            CreateMap<ShortedUrl, UriModel>()
                .ForMember(dest => dest.SourceUri, m => m.MapFrom(src => src.SourceUri))
                .ForMember(dest => dest.ShortUri, m => m.MapFrom(src => ShortUriGenerator.GenerateShortUriByKey(src.ShortUri)))
                .ForMember(dest => dest.CreatedAt, m => m.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.TransferCount, m => m.MapFrom(src => src.TransferCount));
        }
    }
}
