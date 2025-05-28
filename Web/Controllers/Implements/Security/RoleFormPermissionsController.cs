using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.Security.RolFormPermission;
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
    public class RoleFormPermissionsController : ControllerBase
    {
        private readonly IRolFormPermissionBusiness _rolFormPermissionBusiness;
        private readonly ILogger<RoleFormPermissionsController> _logger;

        public RoleFormPermissionsController(
            IRolFormPermissionBusiness rolFormPermissionBusiness,
            ILogger<RoleFormPermissionsController> logger)
        {
            _rolFormPermissionBusiness = rolFormPermissionBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las asignaciones de permisos a roles por formulario
        /// </summary>
        /// <returns>Lista de asignaciones de permisos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RolFormPermissionDto>>> GetAll()
        {
            try
            {
                var permissions = await _rolFormPermissionBusiness.GetAllAsync();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener asignaciones de permisos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene una asignación de permiso por su ID
        /// </summary>
        /// <param name="id">ID de la asignación</param>
        /// <returns>Información de la asignación</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RolFormPermissionDto>> GetById(int id)
        {
            try
            {
                var permission = await _rolFormPermissionBusiness.GetByIdAsync(id);
                if (permission == null)
                {
                    return NotFound($"Asignación con ID {id} no encontrada");
                }

                return Ok(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener asignación {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea una nueva asignación de permiso a rol por formulario
        /// </summary>
        /// <param name="roleFormPermissionDto">Datos de la asignación a crear</param>
        /// <returns>Asignación creada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RolFormPermissionDto>> Create([FromBody] CreateRolFormPermissionDto roleFormPermissionDto)
        {
            try
            {
                var createdPermission = await _rolFormPermissionBusiness.CreateAsync(roleFormPermissionDto);
                return CreatedAtAction(nameof(GetById), new { id = createdPermission.Id }, createdPermission);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear asignación: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear asignación: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina una asignación de permiso
        /// </summary>
        /// <param name="id">ID de la asignación a eliminar</param>
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
                var success = await _rolFormPermissionBusiness.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Asignación con ID {id} no encontrada");
                }

                return Ok("Asignación eliminada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar asignación {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todas las asignaciones de permisos activas
        /// </summary>
        /// <returns>Lista de asignaciones de permisos activas</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RolFormPermissionDto>>> GetActive()
        {
            try
            {
                var permissions = await _rolFormPermissionBusiness.GetActivePermissionsAsync();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener asignaciones activas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todas las asignaciones de permisos por rol
        /// </summary>
        /// <param name="roleId">ID del rol</param>
        /// <returns>Lista de asignaciones de permisos para el rol especificado</returns>
        [HttpGet("byRole/{roleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RolFormPermissionDto>>> GetByRole(int roleId)
        {
            try
            {
                var permissions = await _rolFormPermissionBusiness.GetByRoleAsync(roleId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener asignaciones por rol {roleId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todas las asignaciones de permisos por formulario
        /// </summary>
        /// <param name="formId">ID del formulario</param>
        /// <returns>Lista de asignaciones de permisos para el formulario especificado</returns>
        [HttpGet("byForm/{formId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RolFormPermissionDto>>> GetByForm(int formId)
        {
            try
            {
                var permissions = await _rolFormPermissionBusiness.GetByFormAsync(formId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener asignaciones por formulario {formId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todas las asignaciones de permisos por tipo de permiso
        /// </summary>
        /// <param name="permissionId">ID del permiso</param>
        /// <returns>Lista de asignaciones para el permiso especificado</returns>
        [HttpGet("byPermission/{permissionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RolFormPermissionDto>>> GetByPermission(int permissionId)
        {
            try
            {
                var permissions = await _rolFormPermissionBusiness.GetByPermissionAsync(permissionId);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener asignaciones por permiso {permissionId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Verifica si un rol tiene un permiso específico para un formulario
        /// </summary>
        /// <param name="roleId">ID del rol</param>
        /// <param name="formId">ID del formulario</param>
        /// <param name="permissionId">ID del permiso</param>
        /// <returns>Asignación encontrada o NotFound si no existe</returns>
        [HttpGet("verify/{roleId}/{formId}/{permissionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RolFormPermissionDto>> VerifyPermission(int roleId, int formId, int permissionId)
        {
            try
            {
                var permission = await _rolFormPermissionBusiness.GetByRoleFormPermissionAsync(roleId, formId, permissionId);
                if (permission == null)
                {
                    return NotFound("No se encontró la asignación del permiso especificado");
                }

                return Ok(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al verificar permiso: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Asigna múltiples permisos a un rol para un formulario específico
        /// </summary>
        /// <param name="assignPermissionsDto">Datos de asignación masiva</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPost("batch-assign")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> BatchAssignPermissions([FromBody] BatchAssignRolFormPermissionDto assignPermissionsDto)
        {
            try
            {
                await _rolFormPermissionBusiness.BatchAssignPermissionsAsync(assignPermissionsDto);
                return Ok("Permisos asignados correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al asignar permisos: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al asignar permisos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
