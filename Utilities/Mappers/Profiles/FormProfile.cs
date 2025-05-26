using AutoMapper;
using Entity.Dtos.Security.Form;
using Entity.Model;

namespace Utilities.Mappers.Profiles
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
