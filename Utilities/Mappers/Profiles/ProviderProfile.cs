using AutoMapper;
using Entity.Dtos.OtherDatesPerson.Provider;
using Entity.Model;

namespace Utilities.Mappers.Profiles
{
    /// <summary>
    /// Perfil de mapeo para la entidad Provider y sus DTOs relacionados
    /// </summary>
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            // Mapeo de Provider a ProviderDto y viceversa
            CreateMap<Provider, ProviderDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateProviderDto, Provider>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalProviderDto, Provider>();
        }
    }
}
