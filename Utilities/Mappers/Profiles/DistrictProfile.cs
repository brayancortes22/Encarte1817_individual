using AutoMapper;
using Entity.Dtos.OtherDatesPerson.District;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad District y sus DTOs relacionados
    /// </summary>
    public class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            // Mapeo de District a DistrictDto y viceversa
            CreateMap<District, DistrictDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateDistrictDto, District>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalDistrictDto, District>();
        }
    }
}
