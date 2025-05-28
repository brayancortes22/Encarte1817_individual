using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Base;
using Entity.Model.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Implements
{
    public abstract class ABaseBusiness<T, D> : IBaseBusiness<T, D> where T : BaseEntity where D : BaseDto
    {
        /// <summary>
        /// Obtiene una colección de todas las entidades activas.
        /// </summary>
        /// <returns>
        /// Tarea asíncrona que representa la operación y contiene una colección de entidades 
        /// cuando se completa correctamente.
        /// </returns>
        public abstract Task<List<D>> GetAllAsync();

        /// <summary>
        /// Obtiene una colección de todas las entidades (activas e inactivas).
        /// </summary>
        /// <returns>
        /// Tarea asíncrona que representa la operación y contiene una colección completa de entidades.
        /// </returns>
        public abstract Task<List<D>> GetAllWithInactiveAsync();

        /// <summary>
        /// Recupera una entidad específica por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a recuperar.</param>
        /// <returns>
        /// Tarea asíncrona que representa la operación y contiene la entidad solicitada
        /// cuando se completa correctamente. Puede retornar null si no se encuentra la entidad.
        /// </returns>
        public abstract Task<D?> GetByIdAsync(int id);

        /// <summary>
        /// Crea una nueva entidad en la fuente de datos.
        /// </summary>
        /// <param name="dto">DTO con los datos para crear. No debe ser null.</param>
        /// <returns>
        /// Tarea asíncrona que representa la operación y contiene la entidad creada con sus
        /// valores actualizados (como el ID generado) cuando se completa correctamente.
        /// </returns>
        public abstract Task<D> CreateAsync(D dto);

        /// <summary>
        /// Actualiza una entidad existente en la fuente de datos.
        /// </summary>
        /// <param name="dto">DTO con los valores actualizados. No debe ser null.</param>
        /// <returns>
        /// Tarea asíncrona que representa la operación y contiene la entidad actualizada
        /// cuando se completa correctamente.
        /// </returns>
        public abstract Task<D> UpdateAsync(D dto);

        /// <summary>
        /// Actualiza parcialmente una entidad existente (solo los campos proporcionados)
        /// </summary>
        /// <param name="id">ID de la entidad a actualizar</param>
        /// <param name="propertyValues">Diccionario con los nombres de las propiedades y sus nuevos valores</param>
        /// <returns>DTO actualizado o null si no se encuentra</returns>
        public abstract Task<D?> UpdatePartialAsync(int id, Dictionary<string, object> propertyValues);

        /// <summary>
        /// Elimina permanentemente una entidad de la fuente de datos por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único de la entidad a eliminar.</param>
        /// <returns>
        /// Tarea asíncrona que representa la operación y contiene un valor booleano que indica
        /// si la eliminación fue exitosa (true) o si la entidad no existía (false).
        /// </returns>
        public abstract Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Elimina lógicamente una entidad (cambio de estado a inactivo)
        /// </summary>
        /// <param name="id">ID de la entidad a desactivar</param>
        /// <returns>True si se desactivó correctamente, False si no se encontró</returns>
        public abstract Task<bool> SoftDeleteAsync(int id);

        /// <summary>
        /// Busca entidades que cumplan con una condición específica
        /// </summary>
        /// <param name="predicate">Expresión que define la condición de búsqueda</param>
        /// <returns>Lista de DTOs que cumplen la condición</returns>
        public abstract Task<List<D>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla la condición especificada
        /// </summary>
        /// <param name="predicate">Expresión que define la condición</param>
        /// <returns>True si existe al menos una entidad que cumple la condición</returns>
        public abstract Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
