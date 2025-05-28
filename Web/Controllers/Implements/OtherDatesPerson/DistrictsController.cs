using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.District;
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
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictBusiness _districtBusiness;
        private readonly ILogger<DistrictsController> _logger;

        public DistrictsController(IDistrictBusiness districtBusiness, ILogger<DistrictsController> logger)
        {
            _districtBusiness = districtBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los distritos activos
        /// </summary>
        /// <returns>Lista de distritos activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetActiveDistricts()
        {
            try
            {
                var districts = await _districtBusiness.GetActiveDistrictsAsync();
                return Ok(districts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener distritos activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los distritos de una ciudad específica
        /// </summary>
        /// <param name="cityId">ID de la ciudad</param>
        /// <returns>Lista de distritos de la ciudad</returns>
        [HttpGet("bycity/{cityId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetDistrictsByCity(int cityId)
        {
            try
            {
                var districts = await _districtBusiness.GetDistrictsByCityAsync(cityId);
                return Ok(districts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener distritos de la ciudad {cityId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los distritos (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de distritos</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DistrictDto>>> GetAll()
        {
            try
            {
                var districts = await _districtBusiness.GetAllAsync();
                return Ok(districts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener distritos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un distrito específico por su ID
        /// </summary>
        /// <param name="id">ID del distrito</param>
        /// <returns>Datos del distrito solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DistrictDto>> GetById(int id)
        {
            try
            {
                var district = await _districtBusiness.GetByIdAsync(id);
                if (district == null)
                    return NotFound($"Distrito con ID {id} no encontrado");
                
                return Ok(district);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener distrito con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo distrito en el sistema
        /// </summary>
        /// <param name="districtDto">Datos del distrito a crear</param>
        /// <returns>Datos del distrito creado con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DistrictDto>> Create([FromBody] DistrictDto districtDto)
        {
            try
            {
                if (districtDto == null)
                    return BadRequest("Los datos del distrito son inválidos");

                var createdDistrict = await _districtBusiness.CreateAsync(districtDto);
                return CreatedAtAction(nameof(GetById), new { id = createdDistrict.Id }, createdDistrict);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear distrito: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear distrito: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de un distrito existente
        /// </summary>
        /// <param name="id">ID del distrito a actualizar</param>
        /// <param name="districtDto">Datos actualizados del distrito</param>
        /// <returns>Datos del distrito actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DistrictDto>> Update(int id, [FromBody] DistrictDto districtDto)
        {
            try
            {
                if (districtDto == null)
                    return BadRequest("Los datos del distrito son inválidos");

                if (id != districtDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingDistrict = await _districtBusiness.GetByIdAsync(id);
                if (existingDistrict == null)
                    return NotFound($"Distrito con ID {id} no encontrado");

                var updatedDistrict = await _districtBusiness.UpdateAsync(districtDto);
                return Ok(updatedDistrict);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar distrito: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar distrito con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un distrito
        /// </summary>
        /// <param name="updateDistrictDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateDistrictDto updateDistrictDto)
        {
            try
            {
                if (updateDistrictDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _districtBusiness.UpdatePartialDistrictAsync(updateDistrictDto);
                if (!success)
                    return NotFound($"Distrito con ID {updateDistrictDto.Id} no encontrado");

                return Ok("Distrito actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente distrito: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente distrito: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de un distrito (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalDistrictDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _districtBusiness.DeleteLogicDistrictAsync(deleteDto);
                if (!success)
                    return NotFound($"Distrito con ID {deleteDto.Id} no encontrado");

                return Ok("Estado del distrito actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente distrito: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente distrito: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un distrito del sistema
        /// </summary>
        /// <param name="id">ID del distrito a eliminar</param>
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
                var success = await _districtBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"Distrito con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar distrito con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
