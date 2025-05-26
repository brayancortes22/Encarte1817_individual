using AutoMapper;
using Entity.Dtos.Security.FormModule;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    public class FormModuleProfile : Profile
    {
        public FormModuleProfile()
        {
            // Mapeo de FormModule a FormModuleDto y viceversa
            CreateMap<FormModule, FormModuleDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateFormModuleDto, FormModule>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalFormModuleDto, FormModule>();
        }
    }
}
