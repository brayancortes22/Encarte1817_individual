using Entity.Dtos.Security.RolFormPermission;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de permisos de roles sobre formularios.
    /// </summary>
    public interface IRolFormPermissionBusiness : IBaseBusiness<RolFormPermission, RolFormPermissionDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un permiso de rol sobre formulario.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del permiso</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialRolFormPermissionAsync(UpdateRolFormPermissionDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico de un permiso de rol sobre formulario, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del permiso a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicRolFormPermissionAsync(DeleteLogicalRolFormPermissionDto dto);
        
        /// <summary>
        /// Obtiene todos los permisos de roles sobre formularios activos.
        /// </summary>
        /// <returns>Lista de DTOs de permisos de roles sobre formularios activos</returns>
        Task<List<RolFormPermissionDto>> GetActiveRolFormPermissionsAsync();
        
        /// <summary>
        /// Obtiene los permisos por ID de rol.
        /// </summary>
        /// <param name="rolId">ID del rol</param>
        /// <returns>Lista de DTOs de permisos asociados al rol</returns>
        Task<List<RolFormPermissionDto>> GetByRolIdAsync(int rolId);
        
        /// <summary>
        /// Obtiene los permisos por ID de formulario.
        /// </summary>
        /// <param name="formId">ID del formulario</param>
        /// <returns>Lista de DTOs de permisos asociados al formulario</returns>
        Task<List<RolFormPermissionDto>> GetByFormIdAsync(int formId);
        
        /// <summary>
        /// Obtiene los permisos por ID de tipo de permiso.
        /// </summary>
        /// <param name="permissionId">ID del tipo de permiso</param>
        /// <returns>Lista de DTOs de permisos del tipo especificado</returns>
        Task<List<RolFormPermissionDto>> GetByPermissionIdAsync(int permissionId);
        
        /// <summary>
        /// Obtiene un permiso específico por la combinación de rol, formulario y tipo de permiso.
        /// </summary>
        /// <param name="rolId">ID del rol</param>
        /// <param name="formId">ID del formulario</param>
        /// <param name="permissionId">ID del tipo de permiso</param>
        /// <returns>DTO del permiso si existe; de lo contrario, null</returns>
        Task<RolFormPermissionDto> GetByRolFormPermissionAsync(int rolId, int formId, int permissionId);
    }
}
