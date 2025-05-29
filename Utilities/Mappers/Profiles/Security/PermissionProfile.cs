using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.Security
{
    /// <summary>
    /// Perfil de mapeo para la entidad Permission y sus DTOs relacionados
    /// </summary>
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            // Mapeo de Permission a PermissionDto y viceversa
            CreateMap<Permission, PermissionDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdatePermissionDto, Permission>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalPermissionDto, Permission>();
        }
    }
}
