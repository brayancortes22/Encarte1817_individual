using AutoMapper;
using Entity.Dtos.OtherDatesPerson.Employee;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad Employee y sus DTOs relacionados
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Mapeo de Employee a EmployeeDto y viceversa
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateEmployeeDto, Employee>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalEmployeeDto, Employee>();
        }
    }
}
