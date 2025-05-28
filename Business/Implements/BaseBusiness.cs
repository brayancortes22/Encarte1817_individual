using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Microsoft.Extensions.Logging;
using Utilities.Interfaces;
using Data.Interfaces;
using FluentValidation.Results;
using Entity.Dtos.Base;
using Entity.Model.Base;

namespace Business.Implements
{
    /// <summary>
    /// Clase base que implementa la lógica de negocio común para operaciones CRUD genéricas.
    /// Proporciona implementaciones estándar para crear, leer, actualizar y eliminar entidades,
    /// incluyendo validación, mapeo automático entre DTOs y entidades, y logging.
    /// </summary>
    /// <typeparam name="TDto">Tipo del objeto de transferencia de datos (DTO) utilizado para comunicación con capas superiores</typeparam>
    /// <typeparam name="TEntity">Tipo de la entidad de dominio que representa el modelo de datos</typeparam>
    /// <remarks>
    /// Esta clase hereda de ABaseBusiness y extiende su funcionalidad añadiendo:
    /// - Mapeo automático entre DTOs y entidades usando AutoMapper
    /// - Validación de DTOs usando FluentValidation
    /// - Logging detallado de todas las operaciones
    /// - Manejo consistente de errores
    /// </remarks>
    public class BaseBusiness<T, D> : ABaseBusiness<T, D> where T : BaseEntity where D : BaseDto
    {
        /// <summary>
        /// Instancia de AutoMapper para realizar el mapeo entre DTOs y entidades.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// Servicio de utilidades genéricas que incluye funcionalidades como validación.
        /// </summary>
        protected readonly IGenericIHelpers _helpers;

        /// <summary>
        /// Datos del modelo base encapsulados de solo lectura.
        /// Proporciona acceso seguro a la información de la entidad tipo T.
        /// </summary>
        protected readonly IBaseModelData<T> _data;

        /// <summary>
        /// Logger para registrar eventos, errores y operaciones de la clase.
        /// Inyectado por dependencia y accesible desde clases derivadas.
        /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Inicializa una nueva instancia de la clase BaseBusiness.
        /// </summary>
        /// <param name="repository">Repositorio de datos para operaciones de persistencia de la entidad</param>
        /// <param name="logger">Logger para registrar eventos y errores durante las operaciones</param>
        /// <param name="mapper">Instancia de AutoMapper para mapeo entre DTOs y entidades</param>
        /// <param name="helpers">Servicio de utilidades que proporciona funcionalidades como validación</param>
        public BaseBusiness(
            IBaseModelData<T> data,
            IMapper mapper,
            ILogger logger,
            IGenericIHelpers helpers)
            : base()
        {
            _data = data;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
            _helpers = helpers ?? throw new ArgumentNullException(nameof(helpers));
        }

        /// <summary>
        /// Valida un DTO utilizando las reglas de validación de FluentValidation.
        /// </summary>
        /// <param name="dto">El objeto DTO a validar</param>
        /// <returns>Una tarea que representa la operación de validación asíncrona</returns>
        /// <remarks>
        /// Este método utiliza el servicio _helpers para realizar la validación.
        /// Si la validación falla, se agrupan todos los errores en una sola excepción.
        /// </remarks>
        protected async Task EnsureValid(D dto)
        {
            var validationResult = await _helpers.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors);
                throw new ArgumentException($"Validación fallida: {errors}");
            }
        }

        /// <summary>
        /// Obtiene todos los registros activos de la entidad desde el repositorio.
        /// </summary>
        /// <returns>
        /// Una lista de DTOs que representan todas las entidades activas
        /// </returns>
        public override async Task<List<D>> GetAllAsync()
        {
            try
            {
                var entities = await _data.GetAllAsync();
                _logger.LogInformation($"Obteniendo todos los registros activos de {typeof(T).Name}");
                return _mapper.Map<IList<D>>(entities).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener registros de {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los registros (activos e inactivos) de la entidad desde el repositorio.
        /// </summary>
        /// <returns>
        /// Una lista completa de DTOs que representan todas las entidades
        /// </returns>
        public override async Task<List<D>> GetAllWithInactiveAsync()
        {
            try
            {
                var entities = await _data.GetAllWithInactiveAsync();
                _logger.LogInformation($"Obteniendo todos los registros (activos e inactivos) de {typeof(T).Name}");
                return _mapper.Map<IList<D>>(entities).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los registros de {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene una entidad específica por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único de la entidad a buscar</param>
        /// <returns>
        /// El DTO correspondiente a la entidad encontrada, o null si no existe
        /// </returns>
        public override async Task<D?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _data.GetByIdAsync(id);
                _logger.LogInformation($"Obteniendo {typeof(T).Name} con ID: {id}");
                return entity != null ? _mapper.Map<D>(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener {typeof(T).Name} con ID {id}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Crea una nueva entidad en el sistema a partir de un DTO.
        /// </summary>
        /// <param name="dto">El DTO que contiene los datos para crear la nueva entidad</param>
        /// <returns>
        /// El DTO de la entidad creada, incluyendo el ID asignado y cualquier otro campo generado
        /// </returns>
        public override async Task<D> CreateAsync(D dto)
        {
            try
            {
                await EnsureValid(dto);
                var entity = _mapper.Map<T>(dto);
                entity = await _data.CreateAsync(entity);
                _logger.LogInformation($"Creando nuevo {typeof(T).Name}");
                return _mapper.Map<D>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear {typeof(T).Name} desde DTO: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza una entidad existente con los datos proporcionados en el DTO.
        /// </summary>
        /// <param name="dto">El DTO que contiene los datos actualizados para la entidad</param>
        /// <returns>
        /// El DTO de la entidad actualizada
        /// </returns>
        public override async Task<D> UpdateAsync(D dto)
        {
            try
            {
                await EnsureValid(dto);
                var entity = _mapper.Map<T>(dto);
                entity = await _data.UpdateAsync(entity);
                _logger.LogInformation($"Actualizando {typeof(T).Name} con ID: {entity.Id}");
                return _mapper.Map<D>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar {typeof(T).Name} desde DTO: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente una entidad existente (solo los campos proporcionados)
        /// </summary>
        /// <param name="id">ID de la entidad a actualizar</param>
        /// <param name="propertyValues">Diccionario con los nombres de las propiedades y sus nuevos valores</param>
        /// <returns>El DTO de la entidad actualizada o null si no se encuentra</returns>
        public override async Task<D?> UpdatePartialAsync(int id, Dictionary<string, object> propertyValues)
        {
            try
            {
                var entity = await _data.UpdatePartialAsync(id, propertyValues);
                _logger.LogInformation($"Actualizando parcialmente {typeof(T).Name} con ID: {id}");
                return entity != null ? _mapper.Map<D>(entity) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente {typeof(T).Name} con ID {id}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Elimina permanentemente una entidad del sistema por su identificador.
        /// </summary>
        /// <param name="id">El identificador único de la entidad a eliminar</param>
        /// <returns>
        /// true si la entidad fue eliminada exitosamente; false en caso contrario
        /// </returns>
        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando permanentemente {typeof(T).Name} con ID: {id}");
                return await _data.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar permanentemente {typeof(T).Name} con ID {id}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Elimina lógicamente una entidad (cambio de estado a inactivo)
        /// </summary>
        /// <param name="id">ID de la entidad a desactivar</param>
        /// <returns>True si se desactivó correctamente, False si no se encontró</returns>
        public override async Task<bool> SoftDeleteAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Eliminando lógicamente {typeof(T).Name} con ID: {id}");
                return await _data.SoftDeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente {typeof(T).Name} con ID {id}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca entidades que cumplan con una condición específica
        /// </summary>
        /// <param name="predicate">Expresión lambda que define la condición de búsqueda</param>
        /// <returns>Lista de DTOs que cumplen la condición</returns>
        public override async Task<List<D>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var entities = await _data.FindAsync(predicate);
                _logger.LogInformation($"Buscando {typeof(T).Name} que cumplen condición específica");
                return _mapper.Map<IList<D>>(entities).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar {typeof(T).Name} por condición: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla con la condición especificada
        /// </summary>
        /// <param name="predicate">Expresión lambda que define la condición</param>
        /// <returns>True si existe al menos una entidad que cumple la condición</returns>
        public override async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                _logger.LogInformation($"Verificando existencia de {typeof(T).Name} que cumple condición específica");
                return await _data.ExistsAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al verificar existencia de {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }
    }
}
