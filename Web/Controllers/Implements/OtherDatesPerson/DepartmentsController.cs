using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.Department;
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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentBusiness _departmentBusiness;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentBusiness departmentBusiness, ILogger<DepartmentsController> logger)
        {
            _departmentBusiness = departmentBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los departamentos activos
        /// </summary>
        /// <returns>Lista de departamentos activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetActiveDepartments()
        {
            try
            {
                var departments = await _departmentBusiness.GetActiveDepartmentsAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener departamentos activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los departamentos de un país específico
        /// </summary>
        /// <param name="countryId">ID del país</param>
        /// <returns>Lista de departamentos del país</returns>
        [HttpGet("bycountry/{countryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartmentsByCountry(int countryId)
        {
            try
            {
                var departments = await _departmentBusiness.GetDepartmentsByCountryAsync(countryId);
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener departamentos del país {countryId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los departamentos (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de departamentos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            try
            {
                var departments = await _departmentBusiness.GetAllAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener departamentos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un departamento específico por su ID
        /// </summary>
        /// <param name="id">ID del departamento</param>
        /// <returns>Datos del departamento solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            try
            {
                var department = await _departmentBusiness.GetByIdAsync(id);
                if (department == null)
                    return NotFound($"Departamento con ID {id} no encontrado");
                
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener departamento con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo departamento en el sistema
        /// </summary>
        /// <param name="departmentDto">Datos del departamento a crear</param>
        /// <returns>Datos del departamento creado con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto == null)
                    return BadRequest("Los datos del departamento son inválidos");

                var createdDepartment = await _departmentBusiness.CreateAsync(departmentDto);
                return CreatedAtAction(nameof(GetById), new { id = createdDepartment.Id }, createdDepartment);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear departamento: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear departamento: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de un departamento existente
        /// </summary>
        /// <param name="id">ID del departamento a actualizar</param>
        /// <param name="departmentDto">Datos actualizados del departamento</param>
        /// <returns>Datos del departamento actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DepartmentDto>> Update(int id, [FromBody] DepartmentDto departmentDto)
        {
            try
            {
                if (departmentDto == null)
                    return BadRequest("Los datos del departamento son inválidos");

                if (id != departmentDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingDepartment = await _departmentBusiness.GetByIdAsync(id);
                if (existingDepartment == null)
                    return NotFound($"Departamento con ID {id} no encontrado");

                var updatedDepartment = await _departmentBusiness.UpdateAsync(departmentDto);
                return Ok(updatedDepartment);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar departamento: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar departamento con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un departamento
        /// </summary>
        /// <param name="updateDepartmentDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                if (updateDepartmentDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _departmentBusiness.UpdatePartialDepartmentAsync(updateDepartmentDto);
                if (!success)
                    return NotFound($"Departamento con ID {updateDepartmentDto.Id} no encontrado");

                return Ok("Departamento actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente departamento: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente departamento: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de un departamento (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalDepartmentDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _departmentBusiness.DeleteLogicDepartmentAsync(deleteDto);
                if (!success)
                    return NotFound($"Departamento con ID {deleteDto.Id} no encontrado");

                return Ok("Estado del departamento actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente departamento: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente departamento: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un departamento del sistema
        /// </summary>
        /// <param name="id">ID del departamento a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _departmentBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"Departamento con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar departamento con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
