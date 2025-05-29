using Data.Interfaces;
using Data.Interfaces.Security;
using Data.Implements.BaseData;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.Security
{
    /// <summary>
    /// Implementación de operaciones de datos para entidades User (usuario)
    /// </summary>
    public class UserData : BaseModelData<User>, IUserData
    {
        public UserData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para User si son necesarios
        // Por ejemplo, búsqueda por email
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Verificación de credenciales
        public async Task<User> ValidateCredentialsAsync(string email, string password)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        // Obtener usuario con información de la persona y roles relacionados
        public async Task<User> GetUserWithDetailsAsync(int id)
        {
            return await _entities
                .Include(u => u.Person)
                .Include(u => u.RolUsers)
                    .ThenInclude(ru => ru.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
