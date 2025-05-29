using AutoMapper;
using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappers.Profiles.Security
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
