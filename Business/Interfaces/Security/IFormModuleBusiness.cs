using Entity.Dtos.Security.FormModule;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de relaciones entre formularios y módulos.
    /// </summary>
    public interface IFormModuleBusiness : IBaseBusiness<FormModule, FormModuleDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de una relación formulario-módulo.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados de la relación</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialFormModuleAsync(UpdateFormModuleDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico de una relación formulario-módulo, marcándola como inactiva.
        /// </summary>
        /// <param name="dto">Objeto con el ID de la relación a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicFormModuleAsync(DeleteLogicalFormModuleDto dto);
        
        /// <summary>
        /// Obtiene todas las relaciones formulario-módulo activas.
        /// </summary>
        /// <returns>Lista de DTOs de relaciones formulario-módulo activas</returns>
        Task<List<FormModuleDto>> GetActiveFormModulesAsync();
        
        /// <summary>
        /// Obtiene las relaciones formulario-módulo por ID de formulario.
        /// </summary>
        /// <param name="formId">ID del formulario</param>
        /// <returns>Lista de DTOs de relaciones formulario-módulo asociadas al formulario</returns>
        Task<List<FormModuleDto>> GetFormModulesByFormIdAsync(int formId);
        
        /// <summary>
        /// Obtiene las relaciones formulario-módulo por ID de módulo.
        /// </summary>
        /// <param name="moduleId">ID del módulo</param>
        /// <returns>Lista de DTOs de relaciones formulario-módulo asociadas al módulo</returns>
        Task<List<FormModuleDto>> GetFormModulesByModuleIdAsync(int moduleId);
    }
}
