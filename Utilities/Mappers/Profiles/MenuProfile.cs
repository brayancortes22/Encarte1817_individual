using AutoMapper;
using Entity.Dtos.Security.Menu;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad Menu y sus DTOs relacionados
    /// </summary>
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            // Mapeo de Menu a MenuDto y viceversa
            CreateMap<Menu, MenuDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateMenuDto, Menu>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalMenuDto, Menu>();
        }
    }
}
