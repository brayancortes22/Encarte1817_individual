using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business.Interfaces;
using Entity.Dtos.Base;
using Entity.Model.Base;
using Microsoft.AspNetCore.Authorization;
using Utilities.Exceptions;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador base genérico que implementa operaciones CRUD estándar para una entidad y su DTO correspondiente.
    /// </summary>
    /// <typeparam name="T">Tipo de entidad que hereda de BaseEntity</typeparam>
    /// <typeparam name="D">Tipo de DTO que hereda de BaseDto</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class GenericController<T, D> : ControllerBase where T : BaseEntity where D : BaseDto
    {
        protected readonly IBaseBusiness<T, D> _business;
        protected readonly ILogger<GenericController<T, D>> _logger;

        /// <summary>
        /// Constructor que inicializa las dependencias necesarias.
        /// </summary>
        /// <param name="business">Servicio de lógica de negocio</param>
        /// <param name="logger">Servicio de logging</param>
        protected GenericController(IBaseBusiness<T, D> business, ILogger<GenericController<T, D>> logger)
        {
            _business = business ?? throw new ArgumentNullException(nameof(business));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Obtiene todos los registros activos.
        /// </summary>
        /// <returns>Lista de DTOs</returns>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<D>>> GetAll()
        {
            try
            {
                var results = await _business.GetAllAsync();
                return Ok(results);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error de negocio al obtener todos los registros");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado al obtener todos los registros");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene un registro por su ID.
        /// </summary>
        /// <param name="id">ID del registro a obtener</param>
        /// <returns>DTO si se encuentra, NotFound si no existe</returns>
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<D>> GetById(int id)
        {
            try
            {
                var result = await _business.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Registro con ID {id} no encontrado" });
                }
                return Ok(result);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, $"Error de negocio al obtener registro con ID {id}");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error no controlado al obtener registro con ID {id}");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Crea un nuevo registro.
        /// </summary>
        /// <param name="dto">DTO con los datos para crear</param>
        /// <returns>DTO del registro creado con su ID asignado</returns>
        [HttpPost]
        public virtual async Task<ActionResult<D>> Create([FromBody] D dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "El objeto recibido es nulo" });
            }

            try
            {
                var result = await _business.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error de negocio al crear registro");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado al crear registro");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Actualiza un registro existente.
        /// </summary>
        /// <param name="id">ID del registro a actualizar</param>
        /// <param name="dto">DTO con los datos actualizados</param>
        /// <returns>DTO actualizado</returns>
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<D>> Update(int id, [FromBody] D dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "El objeto recibido es nulo" });
            }

            if (id != dto.Id)
            {
                return BadRequest(new { message = "El ID de la ruta no coincide con el ID del objeto" });
            }

            try
            {
                var result = await _business.UpdateAsync(dto);
                return Ok(result);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, $"Error de negocio al actualizar registro con ID {id}");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error no controlado al actualizar registro con ID {id}");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Actualiza parcialmente un registro existente.
        /// </summary>
        /// <param name="id">ID del registro a actualizar</param>
        /// <param name="propertyValues">Diccionario con las propiedades a actualizar</param>
        /// <returns>DTO actualizado</returns>
        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<D>> UpdatePartial(int id, [FromBody] Dictionary<string, object> propertyValues)
        {
            try
            {
                var result = await _business.UpdatePartialAsync(id, propertyValues);
                if (result == null)
                {
                    return NotFound(new { message = $"Registro con ID {id} no encontrado" });
                }
                return Ok(result);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, $"Error de negocio al actualizar parcialmente registro con ID {id}");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error no controlado al actualizar parcialmente registro con ID {id}");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Elimina permanentemente un registro.
        /// </summary>
        /// <param name="id">ID del registro a eliminar</param>
        /// <returns>NoContent si se eliminó correctamente</returns>
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _business.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Registro con ID {id} no encontrado" });
                }
                return NoContent();
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, $"Error de negocio al eliminar registro con ID {id}");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error no controlado al eliminar registro con ID {id}");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Elimina lógicamente un registro (cambio de estado a inactivo).
        /// </summary>
        /// <param name="id">ID del registro a eliminar lógicamente</param>
        /// <returns>NoContent si se eliminó correctamente</returns>
        [HttpDelete("soft/{id}")]
        public virtual async Task<ActionResult> SoftDelete(int id)
        {
            try
            {
                var result = await _business.SoftDeleteAsync(id);
                if (!result)
                {
                    return NotFound(new { message = $"Registro con ID {id} no encontrado" });
                }
                return NoContent();
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, $"Error de negocio al eliminar lógicamente registro con ID {id}");
                return BadRequest(new { message = bex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error no controlado al eliminar lógicamente registro con ID {id}");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
