using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;

namespace Data.Interfaces.Security
{
    public interface IUserData : IBaseModelData<User>
    {
        // Métodos específicos para autenticación
        Task<User?> LoginAsync(string email, string password);
        Task<bool> ChangePasswordAsync(int userId, string password);
        
        // Métodos específicos para gestión de usuarios
        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email, int? excludeId = null);
        Task<bool> SetActiveStatusAsync(int id, bool status);
        Task<bool> AssingRolAsync(int userId, int rolId);
    }
}
