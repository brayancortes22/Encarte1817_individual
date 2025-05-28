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
        /// Obtiene todas las entidades (activas e inactivas) desde la base de datos
        /// </summary>
        /// <returns>Una colección completa de objetos de tipo <typeparamref name="D"/></returns>
        Task<List<D>> GetAllWithInactiveAsync();

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
        /// Actualiza parcialmente un registro existente (solo los campos proporcionados)
        /// </summary>
        /// <param name="id">Identificador del registro a actualizar</param>
        /// <param name="propertyValues">Diccionario con los nombres de las propiedades y sus nuevos valores</param>
        /// <returns>El DTO actualizado o null si no se encuentra</returns>
        Task<D?> UpdatePartialAsync(int id, Dictionary<string, object> propertyValues);

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

        /// <summary>
        /// Busca DTOs que cumplan con una condición específica
        /// </summary>
        /// <param name="predicate">Expresión que define la condición de búsqueda</param>
        /// <returns>Lista de DTOs que cumplen la condición</returns>
        Task<List<D>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe algún registro que cumpla con la condición especificada
        /// </summary>
        /// <param name="predicate">Expresión que define la condición</param>
        /// <returns>True si existe al menos un registro que cumple la condición</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}