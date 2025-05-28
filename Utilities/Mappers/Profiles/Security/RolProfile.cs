using AutoMapper;
using Entity.Model;
using Entity.Dtos.RolDTO;

namespace Utilities.Mappers.Profiles.Security
{
    public class RolProfile : Profile
    {
        public RolProfile()
        {
            // Mapeo de Rol a RolDto y viceversa
            CreateMap<Rol, RolDto>().ReverseMap();
        }
    }
}