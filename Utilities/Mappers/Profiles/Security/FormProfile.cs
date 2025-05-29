using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles.Security
{
    /// <summary>
    /// Perfil de mapeo para la entidad Form y sus DTOs relacionados
    /// </summary>
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            // Mapeo de Form a FormDto y viceversa
            CreateMap<Form, FormDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateFormDto, Form>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalFormDto, Form>();
        }
    }
}
