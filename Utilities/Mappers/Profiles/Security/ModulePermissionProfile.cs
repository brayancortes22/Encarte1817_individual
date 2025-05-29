using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.Security
{
    /// <summary>
    /// Perfil de mapeo para la entidad ModulePermission y sus DTOs relacionados
    /// </summary>
    public class ModulePermissionProfile : Profile
    {
        public ModulePermissionProfile()
        {
            // Mapeo de ModulePermission a ModulePermissionDto y viceversa
            CreateMap<ModulePermission, ModulePermissionDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateModulePermissionDto, ModulePermission>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalModulePermissionDto, ModulePermission>();
        }
    }
}
