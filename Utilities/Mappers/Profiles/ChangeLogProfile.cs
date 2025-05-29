using AutoMapper;
using Entity.Dtos;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad ChangeLog y sus DTOs relacionados
    /// </summary>
    public class ChangeLogProfile : Profile
    {
        public ChangeLogProfile()
        {
            // Mapeo de ChangeLog a ChangeLogDto y viceversa
            CreateMap<ChangeLog, ChangeLogDto>().ReverseMap();
            
        }
    }
}
