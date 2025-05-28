using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.Client;
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
    public class ClientsController : ControllerBase
    {
        private readonly IClientBusiness _clientBusiness;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientBusiness clientBusiness, ILogger<ClientsController> logger)
        {
            _clientBusiness = clientBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los clientes activos
        /// </summary>
        /// <returns>Lista de clientes activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetActiveClients()
        {
            try
            {
                var clients = await _clientBusiness.GetActiveClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener clientes activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Busca un cliente por su número de documento
        /// </summary>
        /// <param name="documentNumber">Número de documento</param>
        /// <returns>Cliente encontrado</returns>
        [HttpGet("bydocument/{documentNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetClientByDocument(string documentNumber)
        {
            try
            {
                var client = await _clientBusiness.GetClientByDocumentNumberAsync(documentNumber);
                if (client == null)
                    return NotFound($"Cliente con documento {documentNumber} no encontrado");
                
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar cliente por documento {documentNumber}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Busca clientes por su nombre
        /// </summary>
        /// <param name="name">Nombre o parte del nombre</param>
        /// <returns>Lista de clientes que coinciden con la búsqueda</returns>
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> SearchClientsByName(string name)
        {
            try
            {
                var clients = await _clientBusiness.SearchClientsByNameAsync(name);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar clientes por nombre {name}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los clientes (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de clientes</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
        {
            try
            {
                var clients = await _clientBusiness.GetAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener clientes: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un cliente específico por su ID
        /// </summary>
        /// <param name="id">ID del cliente</param>
        /// <returns>Datos del cliente solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> GetById(int id)
        {
            try
            {
                var client = await _clientBusiness.GetByIdAsync(id);
                if (client == null)
                    return NotFound($"Cliente con ID {id} no encontrado");
                
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener cliente con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo cliente en el sistema
        /// </summary>
        /// <param name="clientDto">Datos del cliente a crear</param>
        /// <returns>Datos del cliente creado con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> Create([FromBody] ClientDto clientDto)
        {
            try
            {
                if (clientDto == null)
                    return BadRequest("Los datos del cliente son inválidos");

                var createdClient = await _clientBusiness.CreateAsync(clientDto);
                return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear cliente: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear cliente: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de un cliente existente
        /// </summary>
        /// <param name="id">ID del cliente a actualizar</param>
        /// <param name="clientDto">Datos actualizados del cliente</param>
        /// <returns>Datos del cliente actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ClientDto>> Update(int id, [FromBody] ClientDto clientDto)
        {
            try
            {
                if (clientDto == null)
                    return BadRequest("Los datos del cliente son inválidos");

                if (id != clientDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingClient = await _clientBusiness.GetByIdAsync(id);
                if (existingClient == null)
                    return NotFound($"Cliente con ID {id} no encontrado");

                var updatedClient = await _clientBusiness.UpdateAsync(clientDto);
                return Ok(updatedClient);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar cliente: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar cliente con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un cliente
        /// </summary>
        /// <param name="updateClientDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateClientDto updateClientDto)
        {
            try
            {
                if (updateClientDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _clientBusiness.UpdatePartialClientAsync(updateClientDto);
                if (!success)
                    return NotFound($"Cliente con ID {updateClientDto.Id} no encontrado");

                return Ok("Cliente actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente cliente: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente cliente: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de un cliente (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalClientDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _clientBusiness.DeleteLogicClientAsync(deleteDto);
                if (!success)
                    return NotFound($"Cliente con ID {deleteDto.Id} no encontrado");

                return Ok("Estado del cliente actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente cliente: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente cliente: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un cliente del sistema
        /// </summary>
        /// <param name="id">ID del cliente a eliminar</param>
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
                var success = await _clientBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"Cliente con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar cliente con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
