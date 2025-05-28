using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.Security.Permission;
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
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionBusiness _permissionBusiness;
        private readonly ILogger<PermissionsController> _logger;

        public PermissionsController(IPermissionBusiness permissionBusiness, ILogger<PermissionsController> logger)
        {
            _permissionBusiness = permissionBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los permisos del sistema
        /// </summary>
        /// <returns>Lista de permisos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAll()
        {
            try
            {
                var permissions = await _permissionBusiness.GetAllAsync();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un permiso por su ID
        /// </summary>
        /// <param name="id">ID del permiso</param>
        /// <returns>Información del permiso</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PermissionDto>> GetById(int id)
        {
            try
            {
                var permission = await _permissionBusiness.GetByIdAsync(id);
                if (permission == null)
                {
                    return NotFound($"Permiso con ID {id} no encontrado");
                }

                return Ok(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el permiso {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo permiso
        /// </summary>
        /// <param name="permissionDto">Datos del permiso a crear</param>
        /// <returns>Permiso creado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PermissionDto>> Create([FromBody] CreatePermissionDto permissionDto)
        {
            try
            {
                var createdPermission = await _permissionBusiness.CreateAsync(permissionDto);
                return CreatedAtAction(nameof(GetById), new { id = createdPermission.Id }, createdPermission);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear permiso: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear permiso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza un permiso existente
        /// </summary>
        /// <param name="id">ID del permiso a actualizar</param>
        /// <param name="permissionDto">Datos actualizados del permiso</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePermissionDto permissionDto)
        {
            if (id != permissionDto.Id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en los datos");
            }

            try
            {
                var success = await _permissionBusiness.UpdateAsync(permissionDto);
                if (!success)
                {
                    return NotFound($"Permiso con ID {id} no encontrado");
                }

                return Ok("Permiso actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar permiso {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar permiso {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente un permiso existente
        /// </summary>
        /// <param name="id">ID del permiso a actualizar</param>
        /// <param name="permissionDto">Datos parciales del permiso</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial(int id, [FromBody] UpdatePartialPermissionDto permissionDto)
        {
            if (id != permissionDto.Id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en los datos");
            }

            try
            {
                var success = await _permissionBusiness.UpdatePartialPermissionAsync(permissionDto);
                if (!success)
                {
                    return NotFound($"Permiso con ID {id} no encontrado");
                }

                return Ok("Permiso actualizado parcialmente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente permiso {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente permiso {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina un permiso
        /// </summary>
        /// <param name="id">ID del permiso a eliminar</param>
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
                var success = await _permissionBusiness.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Permiso con ID {id} no encontrado");
                }

                return Ok("Permiso eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar permiso {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Cambia el estado activo/inactivo de un permiso
        /// </summary>
        /// <param name="id">ID del permiso</param>
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
                var dto = new DeleteLogicalPermissionDto { Id = id, Status = status };
                var success = await _permissionBusiness.DeleteLogicPermissionAsync(dto);
                
                if (!success)
                {
                    return NotFound($"Permiso con ID {id} no encontrado");
                }

                return Ok($"Estado del permiso actualizado a {(status ? "activo" : "inactivo")}");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al cambiar estado del permiso {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar estado del permiso {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los permisos activos
        /// </summary>
        /// <returns>Lista de permisos activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetActive()
        {
            try
            {
                var permissions = await _permissionBusiness.GetActivePermissionsAsync();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los permisos por tipo
        /// </summary>
        /// <param name="typeId">ID del tipo de permiso</param>
        /// <returns>Lista de permisos del tipo especificado</returns>
        [HttpGet("byType/{typeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetByType(int typeId)
        {
            try
            {
                var permissions = await _permissionBusiness.GetPermissionsByTypeAsync(typeId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos por tipo {typeId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
