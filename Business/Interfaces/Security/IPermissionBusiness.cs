using Entity.Dtos.Security.Permission;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de permisos.
    /// </summary>
    public interface IPermissionBusiness : IBaseBusiness<Permission, PermissionDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un permiso.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del permiso</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialPermissionAsync(UpdatePermissionDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del permiso, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del permiso a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicPermissionAsync(DeleteLogicalPermissionDto dto);
        
        /// <summary>
        /// Obtiene todos los permisos activos.
        /// </summary>
        /// <returns>Lista de DTOs de permisos activos</returns>
        Task<List<PermissionDto>> GetActivePermissionsAsync();
        
        /// <summary>
        /// Obtiene los permisos por tipo.
        /// </summary>
        /// <param name="permissionTypeId">ID del tipo de permiso</param>
        /// <returns>Lista de DTOs de permisos del tipo especificado</returns>
        Task<List<PermissionDto>> GetPermissionsByTypeAsync(int permissionTypeId);
    }
}
