using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Implements.BaseDate;
using Data.Interfaces;
using Entity.Dtos.Base;
using Entity.Model.Base;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Business.Interfaces
{
    public interface IBaseBusiness<T, D> where T : BaseEntity where D : BaseDto
    {
        /// <summary>
        /// Obtiene todas las entidades activas desde la base de datos
        /// </summary>
        /// <returns>Una colección de objetos de tipo <typeparamref name="D"/></returns>
        Task<List<D>> GetAllAsync();

        /// <summary>
        /// Obtiene un DTO específico por su ID
        /// </summary>
        /// <param name="id">Identificador único del DTO</param>
        /// <returns>Un objeto <typeparamref name="D"/> si se encuentra; de lo contrario, <c>null</c></returns>
        Task<D?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva entidad en el sistema a partir de un DTO
        /// </summary>
        /// <param name="dto">Objeto de transferencia con los datos para crear</param>
        /// <returns>El DTO creado con su ID asignado</returns>
        Task<D> CreateAsync(D dto);

        /// <summary>
        /// Actualiza completamente un registro existente a partir de un DTO
        /// </summary>
        /// <param name="dto">Objeto de transferencia con los datos actualizados</param>
        /// <returns>El DTO actualizado o una excepción si falla</returns>
        Task<D> UpdateAsync(D dto);

        /// <summary>
        /// Actualiza parcialmente un registro existente a partir de un DTO
        /// </summary>
        /// <param name="dto">Objeto de transferencia con los datos actualizados</param>
        /// <returns>El DTO actualizado o una excepción si falla</returns>
        Task<D> UpdatePartialAsync(D dto);


        /// <summary>
        /// Elimina permanentemente un registro del sistema
        /// </summary>
        /// <param name="id">Identificador del registro a eliminar</param>
        /// <returns>True si la operación fue exitosa; false en caso contrario</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Elimina lógicamente un registro del sistema (cambio de estado a inactivo)
        /// </summary>
        /// <param name="id">Identificador del registro a desactivar</param>
        /// <returns>True si la operación fue exitosa; false en caso contrario</returns>
        Task<bool> SoftDeleteAsync(int id);

    }
}