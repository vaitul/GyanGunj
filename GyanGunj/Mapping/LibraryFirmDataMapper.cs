using AutoMapper;
using Databases.Domains;
using GyanGunj.ViewModels;

namespace GyanGunj.Mapping
{
    public class LibraryFirmDataMapper
    {
        public IMapper Execute()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<MasterLibraryData, ViewModels.EditLibraryFirm>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.PinCode, opts => opts.MapFrom(src => src.PinCode))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Mobile, opts => opts.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.WebSite, opts => opts.MapFrom(src => src.Website));

                cfg.CreateMap<ViewModels.EditLibraryFirm, MasterLibraryData>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country))
                .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City))
                .ForMember(dest => dest.PinCode, opts => opts.MapFrom(src => src.PinCode))
                .ForMember(dest => dest.Phone, opts => opts.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Mobile, opts => opts.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Website, opts => opts.MapFrom(src => src.WebSite));

            });
            return config.CreateMapper();
        }
    }
}
