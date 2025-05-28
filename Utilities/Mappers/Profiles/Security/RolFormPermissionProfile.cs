using AutoMapper;
using Entity.Dtos.Security.RolFormPermission;
using Entity.Model;

namespace Utilities.Mappers.Profiles.Security
{
    public class RolFormPermissionProfile : Profile
    {
        public RolFormPermissionProfile()
        {
            // Mapeo de RolFormPermission a RolFormPermissionDto y viceversa
            CreateMap<RolFormPermission, RolFormPermissionDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateRolFormPermissionDto, RolFormPermission>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalRolFormPermissionDto, RolFormPermission>();
        }
    }
}
