using AutoMapper;
using Databases.Domains;
using GyanGunj.Mapping;

namespace GyanGunj.Extensions
{
    public static partial class MappingExtensions
    {
        private static IMapper _Mapper;
        public static IMapper Mapper
        {
            get 
            {
                if (_Mapper == null)
                    _Mapper = new LibraryFirmDataMapper().Execute();
                return _Mapper; 
            }
            set { _Mapper = value; }
        }

        public static MasterLibraryData ToEntity(this ViewModels.EditLibraryFirm model)
        {
            return Mapper.Map<ViewModels.EditLibraryFirm, MasterLibraryData>(model);
        }
        public static ViewModels.EditLibraryFirm ToModel(this MasterLibraryData entity)
        {
            return Mapper.Map<MasterLibraryData,ViewModels.EditLibraryFirm>(entity);
        }
    }
}
