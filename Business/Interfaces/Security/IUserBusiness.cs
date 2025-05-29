using Business.Interfaces;
using Entity.Dtos;
using Entity.Model;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserBusiness : IBaseBusiness<User, UserDto>
    {
        Task<UserDto> GetByEmailAsync(string email);
        Task<UserDto> ValidateCredentialsAsync(string email, string password);
        Task<UserDto> GetUserWithDetailsAsync(int id);
        Task<string> AuthenticateAsync(LoginRequestDto loginDto);
        Task<UserDto> UpdatePasswordAsync(int id, UpdatePasswordUserDto passwordDto);
    }
}
    