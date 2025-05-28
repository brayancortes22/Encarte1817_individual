using Data.Implements.BaseDate;
using Data.Interfaces;
using Entity.Context;
using Entity.Model.Base;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Data.Implements.BaseData
{
    public class BaseModelData<T> : ABaseModelData<T> where T : BaseEntity
    {
        public BaseModelData(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los registros activos
        /// </summary>
        /// <returns>Lista de entidades activas</returns>
        public override async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => e.Status == true).ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los registros (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de entidades</returns>
        public override async Task<List<T>> GetAllWithInactiveAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        /// <param name="id">ID de la entidad a buscar</param>
        /// <returns>La entidad encontrada o null</returns>
        public override async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="entity">Entidad a crear</param>
        /// <returns>La entidad creada con su ID generado</returns>
        public override async Task<T> CreateAsync(T entity)
        {
            entity.Status = true; // Por defecto, las entidades se crean activas
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza completamente una entidad (todos sus campos)
        /// </summary>
        /// <param name="entity">Entidad con los valores actualizados</param>
        /// <returns>La entidad actualizada</returns>
        public override async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Actualiza parcialmente una entidad (solo los campos proporcionados)
        /// </summary>
        /// <param name="id">ID de la entidad a actualizar</param>
        /// <param name="propertyValues">Diccionario con los nombres de las propiedades y sus nuevos valores</param>
        /// <returns>La entidad actualizada o null si no se encuentra</returns>
        public override async Task<T?> UpdatePartialAsync(int id, Dictionary<string, object> propertyValues)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;

            // Para cada propiedad en el diccionario, actualizar su valor en la entidad
            foreach (var property in propertyValues)
            {
                var propertyInfo = typeof(T).GetProperty(property.Key);
                if (propertyInfo != null && propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(entity, property.Value);
                }
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Eliminación permanente de una entidad
        /// </summary>
        /// <param name="id">ID de la entidad a eliminar</param>
        /// <returns>True si se eliminó correctamente, False si no se encontró</returns>
        public override async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Eliminación lógica (cambio de estado a inactivo)
        /// </summary>
        /// <param name="id">ID de la entidad a desactivar</param>
        /// <returns>True si se desactivó correctamente, False si no se encontró</returns>
        public override async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            entity.Status = false;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Busca entidades que cumplan con una condición específica
        /// </summary>
        /// <param name="predicate">Expresión lambda que define la condición de búsqueda</param>
        /// <returns>Lista de entidades que cumplen la condición</returns>
        public override async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla con la condición especificada
        /// </summary>
        /// <param name="predicate">Expresión lambda que define la condición</param>
        /// <returns>True si existe al menos una entidad que cumple la condición</returns>
        public override async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
