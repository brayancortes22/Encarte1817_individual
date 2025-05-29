using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.Security
{
    /// <summary>
    /// Perfil de mapeo para la entidad Person y sus DTOs relacionados
    /// </summary>
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // Mapeo de Person a PersonDto y viceversa
            CreateMap<Person, PersonDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdatePersonDto, Person>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalPersonDto, Person>();
        }
    }
}
