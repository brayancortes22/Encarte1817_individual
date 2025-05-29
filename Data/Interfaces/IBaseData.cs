using Entity.Model.Base;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Data.Interfaces
{
    /// <summary>
    /// Interfaz que define los métodos generales para operaciones CRUD
    /// </summary>
    /// <typeparam name="T">Tipo de entidad heredada de BaseEntity</typeparam>
    public interface IBaseModelData<T> where T : BaseEntity
    {
        /// <summary>
        /// Método para obtener una entidad por su ID
        /// </summary>
        /// <param name="id">ID de la entidad</param>
        /// <returns>La entidad encontrada o null</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Método para obtener todas las entidades activas
        /// </summary>
        /// <returns>Lista de entidades activas</returns>
        Task<List<T>> GetAllAsync();
        
        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="entity">Entidad a crear</param>
        /// <returns>Entidad creada con su ID asignado</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Actualiza todos los valores de una entidad
        /// </summary>
        /// <param name="entity">Entidad con los nuevos valores</param>
        /// <returns>Entidad actualizada</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Actualiza parcialmente una entidad (solo los campos proporcionados)
        /// </summary>
        /// <param name="id">ID de la entidad a actualizar</param>
        /// <param name="propertyValues">Diccionario de propiedades y valores a actualizar</param>
        /// <returns>Entidad actualizada o null si no se encuentra</returns>
        Task<T?> UpdatePartialAsync(int id, Dictionary<string, object> propertyValues);

        /// <summary>
        /// Eliminación concreta o absoluta
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar</param>
        /// <returns>True si se eliminó correctamente</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Eliminación lógica (cambio de estado a inactivo)
        /// </summary>
        /// <param name="id">ID de la entidad a desactivar</param>
        /// <returns>True si se desactivó correctamente</returns>
        Task<bool> SoftDeleteAsync(int id);
      
    }
}