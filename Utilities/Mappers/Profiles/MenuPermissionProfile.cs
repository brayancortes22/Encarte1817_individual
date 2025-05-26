using AutoMapper;
using Entity.Dtos.Security.MenuPermission;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad MenuPermission y sus DTOs relacionados
    /// </summary>
    public class MenuPermissionProfile : Profile
    {
        public MenuPermissionProfile()
        {
            // Mapeo de MenuPermission a MenuPermissionDto y viceversa
            CreateMap<MenuPermission, MenuPermissionDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateMenuPermissionDto, MenuPermission>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalMenuPermissionDto, MenuPermission>();
        }
    }
}
