using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.OtherDatesPerson
{
    /// <summary>
    /// Perfil de mapeo para la entidad Department y sus DTOs relacionados
    /// </summary>
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // Mapeo de Department a DepartmentDto y viceversa
            CreateMap<Department, DepartmentDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateDepartmentDto, Department>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalDepartmentDto, Department>();
        }
    }
}
