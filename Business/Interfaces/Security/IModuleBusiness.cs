using Entity.Dtos.Security.Module;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de módulos.
    /// </summary>
    public interface IModuleBusiness : IBaseBusiness<Module, ModuleDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un módulo.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del módulo</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialModuleAsync(UpdateModuleDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del módulo, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del módulo a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicModuleAsync(DeleteLogicalModuleDto dto);
        
        /// <summary>
        /// Obtiene todos los módulos activos.
        /// </summary>
        /// <returns>Lista de DTOs de módulos activos</returns>
        Task<List<ModuleDto>> GetActiveModulesAsync();
        
        /// <summary>
        /// Obtiene los módulos asociados a un rol específico.
        /// </summary>
        /// <param name="roleId">ID del rol</param>
        /// <returns>Lista de DTOs de módulos asociados al rol</returns>
        Task<List<ModuleDto>> GetModulesByRoleAsync(int roleId);
    }
}
