using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.OtherDatesPerson
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
            
            // Aquí puedes agregar mapeos adicionales si los necesitas
            // Por ejemplo:
            // CreateMap<CountryCreateDto, Country>();
            // CreateMap<CountryUpdateDto, Country>();
        }
    }
}
