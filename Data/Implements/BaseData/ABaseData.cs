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

namespace Data.Implements.BaseDate
{
    /// <summary>
    /// Clase abstracta para poder sobreescribir métodos e incluir nuevos métodos sin cambiar la Interfaz
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que hereda de BaseEntity</typeparam>
    public abstract class ABaseModelData<T> : IBaseModelData<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected ABaseModelData(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Métodos abstractos que deben ser implementados por las clases derivadas
        public abstract Task<List<T>> GetAllAsync();
        public abstract Task<T?> GetByIdAsync(int id);
        public abstract Task<T> CreateAsync(T entity);
        public abstract Task<T> UpdateAsync(T entity);
        public abstract Task<bool> DeleteAsync(int id);
        
        // Nuevos métodos abstractos
        public abstract Task<List<T>> GetAllWithInactiveAsync();
        public abstract Task<T?> UpdatePartialAsync(int id, Dictionary<string, object> propertyValues);
        public abstract Task<bool> SoftDeleteAsync(int id);
        public abstract Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public abstract Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
