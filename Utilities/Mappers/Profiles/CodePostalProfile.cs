using AutoMapper;
using Entity.Dtos.OtherDatesPerson.CodePostal;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad CodePostal y sus DTOs relacionados
    /// </summary>
    public class CodePostalProfile : Profile
    {
        public CodePostalProfile()
        {
            // Mapeo de CodePostal a CodePostalDto y viceversa
            CreateMap<CodePostal, CodePostalDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateCodePostalDto, CodePostal>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalCodePostalDto, CodePostal>();
        }
    }
}
