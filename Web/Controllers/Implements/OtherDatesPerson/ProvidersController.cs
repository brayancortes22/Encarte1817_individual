using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.Provider;
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
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderBusiness _providerBusiness;
        private readonly ILogger<ProvidersController> _logger;

        public ProvidersController(IProviderBusiness providerBusiness, ILogger<ProvidersController> logger)
        {
            _providerBusiness = providerBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los proveedores activos
        /// </summary>
        /// <returns>Lista de proveedores activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetActiveProviders()
        {
            try
            {
                var providers = await _providerBusiness.GetActiveProvidersAsync();
                return Ok(providers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener proveedores activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Busca un proveedor por su número de documento o NIT
        /// </summary>
        /// <param name="documentNumber">Número de documento o NIT</param>
        /// <returns>Proveedor encontrado</returns>
        [HttpGet("bydocument/{documentNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProviderDto>> GetProviderByDocument(string documentNumber)
        {
            try
            {
                var provider = await _providerBusiness.GetProviderByDocumentNumberAsync(documentNumber);
                if (provider == null)
                    return NotFound($"Proveedor con documento {documentNumber} no encontrado");
                
                return Ok(provider);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar proveedor por documento {documentNumber}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Busca proveedores por su nombre
        /// </summary>
        /// <param name="name">Nombre o parte del nombre</param>
        /// <returns>Lista de proveedores que coinciden con la búsqueda</returns>
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> SearchProvidersByName(string name)
        {
            try
            {
                var providers = await _providerBusiness.SearchProvidersByNameAsync(name);
                return Ok(providers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar proveedores por nombre {name}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los proveedores (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de proveedores</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetAll()
        {
            try
            {
                var providers = await _providerBusiness.GetAllAsync();
                return Ok(providers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener proveedores: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un proveedor específico por su ID
        /// </summary>
        /// <param name="id">ID del proveedor</param>
        /// <returns>Datos del proveedor solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProviderDto>> GetById(int id)
        {
            try
            {
                var provider = await _providerBusiness.GetByIdAsync(id);
                if (provider == null)
                    return NotFound($"Proveedor con ID {id} no encontrado");
                
                return Ok(provider);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener proveedor con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo proveedor en el sistema
        /// </summary>
        /// <param name="providerDto">Datos del proveedor a crear</param>
        /// <returns>Datos del proveedor creado con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProviderDto>> Create([FromBody] ProviderDto providerDto)
        {
            try
            {
                if (providerDto == null)
                    return BadRequest("Los datos del proveedor son inválidos");

                var createdProvider = await _providerBusiness.CreateAsync(providerDto);
                return CreatedAtAction(nameof(GetById), new { id = createdProvider.Id }, createdProvider);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear proveedor: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear proveedor: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de un proveedor existente
        /// </summary>
        /// <param name="id">ID del proveedor a actualizar</param>
        /// <param name="providerDto">Datos actualizados del proveedor</param>
        /// <returns>Datos del proveedor actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProviderDto>> Update(int id, [FromBody] ProviderDto providerDto)
        {
            try
            {
                if (providerDto == null)
                    return BadRequest("Los datos del proveedor son inválidos");

                if (id != providerDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingProvider = await _providerBusiness.GetByIdAsync(id);
                if (existingProvider == null)
                    return NotFound($"Proveedor con ID {id} no encontrado");

                var updatedProvider = await _providerBusiness.UpdateAsync(providerDto);
                return Ok(updatedProvider);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar proveedor: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar proveedor con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un proveedor
        /// </summary>
        /// <param name="updateProviderDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateProviderDto updateProviderDto)
        {
            try
            {
                if (updateProviderDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _providerBusiness.UpdatePartialProviderAsync(updateProviderDto);
                if (!success)
                    return NotFound($"Proveedor con ID {updateProviderDto.Id} no encontrado");

                return Ok("Proveedor actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente proveedor: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente proveedor: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de un proveedor (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalProviderDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _providerBusiness.DeleteLogicProviderAsync(deleteDto);
                if (!success)
                    return NotFound($"Proveedor con ID {deleteDto.Id} no encontrado");

                return Ok("Estado del proveedor actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente proveedor: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente proveedor: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un proveedor del sistema
        /// </summary>
        /// <param name="id">ID del proveedor a eliminar</param>
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
                var success = await _providerBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"Proveedor con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar proveedor con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
