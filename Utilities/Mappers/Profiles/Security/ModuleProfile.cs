using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.Security
{
    /// <summary>
    /// Perfil de mapeo para la entidad Module y sus DTOs relacionados
    /// </summary>
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            // Mapeo de Module a ModuleDto y viceversa
            CreateMap<Module, ModuleDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateModuleDto, Module>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalModuleDto, Module>();
        }
    }
}
