using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.ModuleDTO;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleBusiness _moduleBusiness;
        private readonly ILogger<ModulesController> _logger;

        public ModulesController(IModuleBusiness moduleBusiness, ILogger<ModulesController> logger)
        {
            _moduleBusiness = moduleBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los módulos activos
        /// </summary>
        /// <returns>Lista de módulos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAll()
        {
            try
            {
                var modules = await _moduleBusiness.GetAllAsync();
                return Ok(modules);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener módulos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un módulo por su ID
        /// </summary>
        /// <param name="id">ID del módulo</param>
        /// <returns>Información del módulo</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModuleDto>> GetById(int id)
        {
            try
            {
                var module = await _moduleBusiness.GetByIdAsync(id);
                if (module == null)
                {
                    return NotFound($"Módulo con ID {id} no encontrado");
                }

                return Ok(module);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el módulo {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo módulo
        /// </summary>
        /// <param name="moduleDto">Datos del módulo a crear</param>
        /// <returns>Módulo creado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModuleDto>> Create([FromBody] CreateModuleDto moduleDto)
        {
            try
            {
                var createdModule = await _moduleBusiness.CreateAsync(moduleDto);
                return CreatedAtAction(nameof(GetById), new { id = createdModule.Id }, createdModule);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear módulo: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear módulo: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza un módulo existente
        /// </summary>
        /// <param name="id">ID del módulo a actualizar</param>
        /// <param name="moduleDto">Datos actualizados del módulo</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateModuleDto moduleDto)
        {
            if (id != moduleDto.Id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en los datos");
            }

            try
            {
                var success = await _moduleBusiness.UpdateAsync(moduleDto);
                if (!success)
                {
                    return NotFound($"Módulo con ID {id} no encontrado");
                }

                return Ok("Módulo actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar módulo {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar módulo {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente un módulo existente
        /// </summary>
        /// <param name="id">ID del módulo a actualizar</param>
        /// <param name="moduleDto">Datos parciales del módulo</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial(int id, [FromBody] UpdatePartialModuleDto moduleDto)
        {
            if (id != moduleDto.Id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en los datos");
            }

            try
            {
                var success = await _moduleBusiness.UpdatePartialAsync(moduleDto);
                if (!success)
                {
                    return NotFound($"Módulo con ID {id} no encontrado");
                }

                return Ok("Módulo actualizado parcialmente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente módulo {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente módulo {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina un módulo
        /// </summary>
        /// <param name="id">ID del módulo a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _moduleBusiness.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Módulo con ID {id} no encontrado");
                }

                return Ok("Módulo eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar módulo {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina lógicamente un módulo (cambio de estado)
        /// </summary>
        /// <param name="id">ID del módulo</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("soft/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SoftDelete(int id)
        {
            try
            {
                var success = await _moduleBusiness.SoftDeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Módulo con ID {id} no encontrado");
                }

                return Ok("Módulo desactivado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al desactivar módulo {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Activa o desactiva un módulo
        /// </summary>
        /// <param name="id">ID del módulo</param>
        /// <param name="status">Estado a establecer (true=activo, false=inactivo)</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("active/{id}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SetActiveStatus(int id, bool status)
        {
            try
            {
                var success = await _moduleBusiness.SetActiveStatusAsync(id, status);
                if (!success)
                {
                    return NotFound($"Módulo con ID {id} no encontrado");
                }

                return Ok($"Estado del módulo actualizado a {(status ? "activo" : "inactivo")}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar estado del módulo {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los módulos asociados a un rol
        /// </summary>
        /// <param name="roleId">ID del rol</param>
        /// <returns>Lista de módulos</returns>
        [HttpGet("byRole/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModulesByRole(int roleId)
        {
            try
            {
                var modules = await _moduleBusiness.GetModulesByRoleAsync(roleId);
                return Ok(modules);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener módulos por rol {roleId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
