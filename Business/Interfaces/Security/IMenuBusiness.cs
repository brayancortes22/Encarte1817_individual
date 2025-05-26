using Entity.Dtos.Security.Menu;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de menús.
    /// </summary>
    public interface IMenuBusiness : IBaseBusiness<Menu, MenuDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un menú.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del menú</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialMenuAsync(UpdateMenuDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del menú, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del menú a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicMenuAsync(DeleteLogicalMenuDto dto);
        
        /// <summary>
        /// Obtiene todos los menús activos.
        /// </summary>
        /// <returns>Lista de DTOs de menús activos</returns>
        Task<List<MenuDto>> GetActiveMenusAsync();
        
        /// <summary>
        /// Obtiene los menús asociados a un rol específico.
        /// </summary>
        /// <param name="roleId">ID del rol</param>
        /// <returns>Lista de DTOs de menús asociados al rol</returns>
        Task<List<MenuDto>> GetMenusByRoleAsync(int roleId);
        
        /// <summary>
        /// Obtiene los menús hijos de un menú padre específico o los menús raíz si parentId es null.
        /// </summary>
        /// <param name="parentId">ID del menú padre o null para obtener menús raíz</param>
        /// <returns>Lista de DTOs de menús</returns>
        Task<List<MenuDto>> GetMenusByParentIdAsync(int? parentId);
    }
}
