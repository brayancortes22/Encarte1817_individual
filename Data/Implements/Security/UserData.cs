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

namespace Data.Implements
{
    public class UserData : BaseModelData<User>, IUserData
    {
        public UserData(ApplicationDbContext context) : base(context)
        {
        }

        // Sobrescribimos el método para incluir la relación con Person
        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Sobrescribimos el método para incluir la relación con Person
        public override async Task<List<User>> GetAllAsync()
        {
            return await _dbSet
                .Where(u => u.Status == true)
                .Include(u => u.Person)
                .ToListAsync();
        }

        /// <summary>
        /// Autenticación de usuario mediante email y contraseña
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <param name="password">Contraseña hasheada</param>
        /// <returns>Usuario autenticado o null si las credenciales son incorrectas</returns>
        public async Task<User?> LoginAsync(string email, string password)
        {
            return await _dbSet
                .Include(u => u.Person)
                .Include(u => u.RolUsers)
                    .ThenInclude(ru => ru.Rol)
                .FirstOrDefaultAsync(u => 
                    u.Email == email && 
                    u.Password == password && 
                    u.Status == true);
        }

        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <param name="password">Nueva contraseña (ya hasheada)</param>
        /// <returns>True si se cambió correctamente</returns>
        public async Task<bool> ChangePasswordAsync(int userId, string password)
        {
            var user = await _dbSet.FindAsync(userId);
            if (user == null) return false;
            
            user.Password = password;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Obtiene un usuario por su email
        /// </summary>
        /// <param name="email">Email a buscar</param>
        /// <returns>Usuario encontrado o null</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Include(u => u.Person)
                .Include(u => u.RolUsers)
                    .ThenInclude(ru => ru.Rol)
                .FirstOrDefaultAsync(u => 
                    u.Email == email && 
                    u.Status == true);
        }

        /// <summary>
        /// Cambia el estado activo/inactivo de un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="status">Nuevo estado (true=activo, false=inactivo)</param>
        /// <returns>True si se cambió correctamente</returns>
        public async Task<bool> SetActiveStatusAsync(int id, bool status)
        {
            var user = await _dbSet.FindAsync(id);
            if (user == null) return false;
            
            user.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Verifica si existe un email en la base de datos
        /// </summary>
        /// <param name="email">Email a verificar</param>
        /// <param name="excludeId">ID de usuario a excluir (opcional, para actualizaciones)</param>
        /// <returns>True si el email ya existe</returns>
        public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                // Para validación en actualización (excluir el ID actual)
                return await _dbSet.AnyAsync(u => u.Email == email && u.Id != excludeId.Value);
            }
            
            // Para validación en creación
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Asigna un rol a un usuario
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <param name="rolId">ID del rol</param>
        /// <returns>True si se asignó correctamente</returns>
        public async Task<bool> AssingRolAsync(int userId, int rolId)
        {
            // Verificar que el usuario y rol existen
            var user = await _dbSet.FindAsync(userId);
            var rol = await _context.Roles.FindAsync(rolId);
            
            if (user == null || rol == null) 
                return false;

            // Verificar si ya existe la asignación
            var exists = await _context.RolUsers
                .AnyAsync(ru => ru.UserId == userId && ru.RolId == rolId);
                
            if (exists)
                return true; // Ya estaba asignado
                
            // Crear la asignación
            var rolUser = new RolUser
            {
                UserId = userId,
                RolId = rolId,
                Status = true
            };
            
            await _context.RolUsers.AddAsync(rolUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
