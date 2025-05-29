using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.OtherDatesPerson
{
    /// <summary>
    /// Perfil de mapeo para la entidad City y sus DTOs relacionados
    /// </summary>
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            // Mapeo de City a CityDto y viceversa
            CreateMap<City, CityDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateCityDto, City>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalCityDto, City>();
        }
    }
}
