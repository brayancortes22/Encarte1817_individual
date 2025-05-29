using Entity.Dtos;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Interface
{
    public interface IUserController : IGenericController<UserDto, User>
    {
        Task<IActionResult> GetUserByEmail(string email);
        Task<IActionResult> UpdatePartialUser(UpdateUserDto dto);
       
    }
}