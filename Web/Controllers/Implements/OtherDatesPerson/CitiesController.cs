using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.City;
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
    public class CitiesController : ControllerBase
    {
        private readonly ICityBusiness _cityBusiness;
        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ICityBusiness cityBusiness, ILogger<CitiesController> logger)
        {
            _cityBusiness = cityBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las ciudades activas
        /// </summary>
        /// <returns>Lista de ciudades activas</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetActiveCities()
        {
            try
            {
                var cities = await _cityBusiness.GetActiveCitiesAsync();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ciudades activas: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todas las ciudades de un departamento específico
        /// </summary>
        /// <param name="departmentId">ID del departamento</param>
        /// <returns>Lista de ciudades del departamento</returns>
        [HttpGet("bydepartment/{departmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCitiesByDepartment(int departmentId)
        {
            try
            {
                var cities = await _cityBusiness.GetCitiesByDepartmentAsync(departmentId);
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ciudades del departamento {departmentId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todas las ciudades (activas e inactivas)
        /// </summary>
        /// <returns>Lista completa de ciudades</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAll()
        {
            try
            {
                var cities = await _cityBusiness.GetAllAsync();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ciudades: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene una ciudad específica por su ID
        /// </summary>
        /// <param name="id">ID de la ciudad</param>
        /// <returns>Datos de la ciudad solicitada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityDto>> GetById(int id)
        {
            try
            {
                var city = await _cityBusiness.GetByIdAsync(id);
                if (city == null)
                    return NotFound($"Ciudad con ID {id} no encontrada");
                
                return Ok(city);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ciudad con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea una nueva ciudad en el sistema
        /// </summary>
        /// <param name="cityDto">Datos de la ciudad a crear</param>
        /// <returns>Datos de la ciudad creada con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityDto>> Create([FromBody] CityDto cityDto)
        {
            try
            {
                if (cityDto == null)
                    return BadRequest("Los datos de la ciudad son inválidos");

                var createdCity = await _cityBusiness.CreateAsync(cityDto);
                return CreatedAtAction(nameof(GetById), new { id = createdCity.Id }, createdCity);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear ciudad: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear ciudad: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de una ciudad existente
        /// </summary>
        /// <param name="id">ID de la ciudad a actualizar</param>
        /// <param name="cityDto">Datos actualizados de la ciudad</param>
        /// <returns>Datos de la ciudad actualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CityDto>> Update(int id, [FromBody] CityDto cityDto)
        {
            try
            {
                if (cityDto == null)
                    return BadRequest("Los datos de la ciudad son inválidos");

                if (id != cityDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingCity = await _cityBusiness.GetByIdAsync(id);
                if (existingCity == null)
                    return NotFound($"Ciudad con ID {id} no encontrada");

                var updatedCity = await _cityBusiness.UpdateAsync(cityDto);
                return Ok(updatedCity);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar ciudad: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar ciudad con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de una ciudad
        /// </summary>
        /// <param name="updateCityDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateCityDto updateCityDto)
        {
            try
            {
                if (updateCityDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _cityBusiness.UpdatePartialCityAsync(updateCityDto);
                if (!success)
                    return NotFound($"Ciudad con ID {updateCityDto.Id} no encontrada");

                return Ok("Ciudad actualizada correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente ciudad: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente ciudad: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de una ciudad (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalCityDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _cityBusiness.DeleteLogicCityAsync(deleteDto);
                if (!success)
                    return NotFound($"Ciudad con ID {deleteDto.Id} no encontrada");

                return Ok("Estado de la ciudad actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente ciudad: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente ciudad: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente una ciudad del sistema
        /// </summary>
        /// <param name="id">ID de la ciudad a eliminar</param>
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
                var success = await _cityBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"Ciudad con ID {id} no encontrada");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar ciudad con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
