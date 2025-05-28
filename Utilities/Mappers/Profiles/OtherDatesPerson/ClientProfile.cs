using AutoMapper;
using Entity.Dtos.OtherDatesPerson.Client;
using Entity.Model;

namespace Utilities.Mappers.Profiles.OtherDatesPerson
{
    /// <summary>
    /// Perfil de mapeo para la entidad Client y sus DTOs relacionados
    /// </summary>
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            // Mapeo de Client a ClientDto y viceversa
            CreateMap<Client, ClientDto>().ReverseMap();
            
            // Mapeo para actualización
            CreateMap<UpdateClientDto, Client>();
            
            // Mapeo para eliminación lógica
            CreateMap<DeleteLogicalClientDto, Client>();
        }
    }
}
