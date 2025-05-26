using Entity.Dtos.Security.MenuPermission;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de permisos sobre menús.
    /// </summary>
    public interface IMenuPermissionBusiness : IBaseBusiness<MenuPermission, MenuPermissionDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un permiso sobre menú.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del permiso</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialMenuPermissionAsync(UpdateMenuPermissionDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico de un permiso sobre menú, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del permiso a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicMenuPermissionAsync(DeleteLogicalMenuPermissionDto dto);
        
        /// <summary>
        /// Obtiene todos los permisos sobre menús activos.
        /// </summary>
        /// <returns>Lista de DTOs de permisos sobre menús activos</returns>
        Task<List<MenuPermissionDto>> GetActiveMenuPermissionsAsync();
        
        /// <summary>
        /// Obtiene los permisos por ID de menú.
        /// </summary>
        /// <param name="menuId">ID del menú</param>
        /// <returns>Lista de DTOs de permisos asociados al menú</returns>
        Task<List<MenuPermissionDto>> GetByMenuIdAsync(int menuId);
        
        /// <summary>
        /// Obtiene los permisos por ID de tipo de permiso.
        /// </summary>
        /// <param name="permissionId">ID del tipo de permiso</param>
        /// <returns>Lista de DTOs de permisos del tipo especificado</returns>
        Task<List<MenuPermissionDto>> GetByPermissionIdAsync(int permissionId);
        
        /// <summary>
        /// Obtiene un permiso específico por la combinación de menú y tipo de permiso.
        /// </summary>
        /// <param name="menuId">ID del menú</param>
        /// <param name="permissionId">ID del tipo de permiso</param>
        /// <returns>DTO del permiso si existe; de lo contrario, null</returns>
        Task<MenuPermissionDto> GetByMenuPermissionAsync(int menuId, int permissionId);
    }
}
