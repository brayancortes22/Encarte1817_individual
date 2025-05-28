using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.Country;
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
    public class CountriesController : ControllerBase
    {
        private readonly ICountryBusiness _countryBusiness;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(ICountryBusiness countryBusiness, ILogger<CountriesController> logger)
        {
            _countryBusiness = countryBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los países activos
        /// </summary>
        /// <returns>Lista de países activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetActiveCountries()
        {
            try
            {
                var countries = await _countryBusiness.GetActiveCountriesAsync();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener países activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los países (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de países</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll()
        {
            try
            {
                var countries = await _countryBusiness.GetAllAsync();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener países: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un país específico por su ID
        /// </summary>
        /// <param name="id">ID del país</param>
        /// <returns>Datos del país solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDto>> GetById(int id)
        {
            try
            {
                var country = await _countryBusiness.GetByIdAsync(id);
                if (country == null)
                    return NotFound($"País con ID {id} no encontrado");
                
                return Ok(country);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener país con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo país en el sistema
        /// </summary>
        /// <param name="countryDto">Datos del país a crear</param>
        /// <returns>Datos del país creado con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDto>> Create([FromBody] CountryDto countryDto)
        {
            try
            {
                if (countryDto == null)
                    return BadRequest("Los datos del país son inválidos");

                var createdCountry = await _countryBusiness.CreateAsync(countryDto);
                return CreatedAtAction(nameof(GetById), new { id = createdCountry.Id }, createdCountry);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear país: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear país: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de un país existente
        /// </summary>
        /// <param name="id">ID del país a actualizar</param>
        /// <param name="countryDto">Datos actualizados del país</param>
        /// <returns>Datos del país actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CountryDto>> Update(int id, [FromBody] CountryDto countryDto)
        {
            try
            {
                if (countryDto == null)
                    return BadRequest("Los datos del país son inválidos");

                if (id != countryDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingCountry = await _countryBusiness.GetByIdAsync(id);
                if (existingCountry == null)
                    return NotFound($"País con ID {id} no encontrado");

                var updatedCountry = await _countryBusiness.UpdateAsync(countryDto);
                return Ok(updatedCountry);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar país: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar país con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un país
        /// </summary>
        /// <param name="updateCountryDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateCountryDto updateCountryDto)
        {
            try
            {
                if (updateCountryDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _countryBusiness.UpdatePartialCountryAsync(updateCountryDto);
                if (!success)
                    return NotFound($"País con ID {updateCountryDto.Id} no encontrado");

                return Ok("País actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente país: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente país: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de un país (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalCountryDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _countryBusiness.DeleteLogicCountryAsync(deleteDto);
                if (!success)
                    return NotFound($"País con ID {deleteDto.Id} no encontrado");

                return Ok("Estado del país actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente país: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente país: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un país del sistema
        /// </summary>
        /// <param name="id">ID del país a eliminar</param>
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
                var success = await _countryBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"País con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar país con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
