using Entity.Dtos.Security.Form;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de formularios.
    /// </summary>
    public interface IFormBusiness : IBaseBusiness<Form, FormDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un formulario.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del formulario</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialFormAsync(UpdateFormDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del formulario, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del formulario a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicFormAsync(DeleteLogicalFormDto dto);
        
        /// <summary>
        /// Obtiene todos los formularios activos.
        /// </summary>
        /// <returns>Lista de DTOs de formularios activos</returns>
        Task<List<FormDto>> GetActiveFormsAsync();
        
        /// <summary>
        /// Obtiene los formularios asociados a un módulo específico.
        /// </summary>
        /// <param name="moduleId">ID del módulo</param>
        /// <returns>Lista de DTOs de formularios asociados al módulo</returns>
        Task<List<FormDto>> GetFormsByModuleAsync(int moduleId);
    }
}
