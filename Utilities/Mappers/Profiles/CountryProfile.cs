using AutoMapper;
using Entity.Dtos.OtherDatesPerson.Country;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad Country y sus DTOs relacionados
    /// </summary>
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            // Mapeo de Country a CountryDto y viceversa
            CreateMap<Country, CountryDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateCountryDto, Country>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalCountryDto, Country>();
        }
    }
}
