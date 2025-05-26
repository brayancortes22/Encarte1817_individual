using Entity.Dtos.Security.ModulePermission;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de permisos sobre módulos.
    /// </summary>
    public interface IModulePermissionBusiness : IBaseBusiness<ModulePermission, ModulePermissionDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un permiso sobre módulo.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del permiso</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialModulePermissionAsync(UpdateModulePermissionDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico de un permiso sobre módulo, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del permiso a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicModulePermissionAsync(DeleteLogicalModulePermissionDto dto);
        
        /// <summary>
        /// Obtiene todos los permisos sobre módulos activos.
        /// </summary>
        /// <returns>Lista de DTOs de permisos sobre módulos activos</returns>
        Task<List<ModulePermissionDto>> GetActiveModulePermissionsAsync();
        
        /// <summary>
        /// Obtiene los permisos por ID de módulo.
        /// </summary>
        /// <param name="moduleId">ID del módulo</param>
        /// <returns>Lista de DTOs de permisos asociados al módulo</returns>
        Task<List<ModulePermissionDto>> GetByModuleIdAsync(int moduleId);
        
        /// <summary>
        /// Obtiene los permisos por ID de tipo de permiso.
        /// </summary>
        /// <param name="permissionId">ID del tipo de permiso</param>
        /// <returns>Lista de DTOs de permisos del tipo especificado</returns>
        Task<List<ModulePermissionDto>> GetByPermissionIdAsync(int permissionId);
        
        /// <summary>
        /// Obtiene un permiso específico por la combinación de módulo y tipo de permiso.
        /// </summary>
        /// <param name="moduleId">ID del módulo</param>
        /// <param name="permissionId">ID del tipo de permiso</param>
        /// <returns>DTO del permiso si existe; de lo contrario, null</returns>
        Task<ModulePermissionDto> GetByModulePermissionAsync(int moduleId, int permissionId);
    }
}
