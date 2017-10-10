namespace UriShortening.BusinessLogic.Mapping
{
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
                .ForMember(dest => dest.ShortUri, m => m.MapFrom(src => ShortUriGenerator.CreateShortUriKey() ));

            CreateMap<ShortedUrl, UriModel>()
                .ForMember(dest => dest.SourceUri, m => m.MapFrom(src => src.SourceUri))
                .ForMember(dest => dest.ShortUri, m => m.MapFrom(src => ShortUriGenerator.GenerateShortUriByKey(src.ShortUri)));
        }
    }
}
