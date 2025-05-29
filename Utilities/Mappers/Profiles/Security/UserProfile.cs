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
            CreateMap<User, AssignUserRolDto>().ReverseMap();
            CreateMap<User, DeleteLogicalUserDto>().ReverseMap();
            CreateMap<User, LoginRequestDto>().ReverseMap();
            CreateMap<User, UpdatePasswordUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            
        }
    }
}
