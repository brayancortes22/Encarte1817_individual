using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.Security.Form;
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
    public class FormsController : ControllerBase
    {
        private readonly IFormBusiness _formBusiness;
        private readonly ILogger<FormsController> _logger;

        public FormsController(IFormBusiness formBusiness, ILogger<FormsController> logger)
        {
            _formBusiness = formBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los formularios del sistema
        /// </summary>
        /// <returns>Lista de formularios</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetAll()
        {
            try
            {
                var forms = await _formBusiness.GetAllAsync();
                return Ok(forms);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener formularios: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un formulario por su ID
        /// </summary>
        /// <param name="id">ID del formulario</param>
        /// <returns>Información del formulario</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FormDto>> GetById(int id)
        {
            try
            {
                var form = await _formBusiness.GetByIdAsync(id);
                if (form == null)
                {
                    return NotFound($"Formulario con ID {id} no encontrado");
                }

                return Ok(form);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el formulario {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo formulario
        /// </summary>
        /// <param name="formDto">Datos del formulario a crear</param>
        /// <returns>Formulario creado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FormDto>> Create([FromBody] CreateFormDto formDto)
        {
            try
            {
                var createdForm = await _formBusiness.CreateAsync(formDto);
                return CreatedAtAction(nameof(GetById), new { id = createdForm.Id }, createdForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear formulario: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear formulario: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza un formulario existente
        /// </summary>
        /// <param name="id">ID del formulario a actualizar</param>
        /// <param name="formDto">Datos actualizados del formulario</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateFormDto formDto)
        {
            if (id != formDto.Id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en los datos");
            }

            try
            {
                var success = await _formBusiness.UpdateAsync(formDto);
                if (!success)
                {
                    return NotFound($"Formulario con ID {id} no encontrado");
                }

                return Ok("Formulario actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar formulario {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar formulario {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente un formulario existente
        /// </summary>
        /// <param name="id">ID del formulario a actualizar</param>
        /// <param name="formDto">Datos parciales del formulario</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial(int id, [FromBody] UpdatePartialFormDto formDto)
        {
            if (id != formDto.Id)
            {
                return BadRequest("El ID en la ruta no coincide con el ID en los datos");
            }

            try
            {
                var success = await _formBusiness.UpdatePartialFormAsync(formDto);
                if (!success)
                {
                    return NotFound($"Formulario con ID {id} no encontrado");
                }

                return Ok("Formulario actualizado parcialmente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente formulario {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente formulario {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina un formulario
        /// </summary>
        /// <param name="id">ID del formulario a eliminar</param>
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
                var success = await _formBusiness.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Formulario con ID {id} no encontrado");
                }

                return Ok("Formulario eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar formulario {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los formularios activos
        /// </summary>
        /// <returns>Lista de formularios activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetActive()
        {
            try
            {
                var forms = await _formBusiness.GetActiveFormsAsync();
                return Ok(forms);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener formularios activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los formularios asociados a un módulo
        /// </summary>
        /// <param name="moduleId">ID del módulo</param>
        /// <returns>Lista de formularios del módulo</returns>
        [HttpGet("byModule/{moduleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<FormDto>>> GetFormsByModule(int moduleId)
        {
            try
            {
                var forms = await _formBusiness.GetFormsByModuleAsync(moduleId);
                return Ok(forms);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener formularios por módulo {moduleId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Cambia el estado activo/inactivo de un formulario
        /// </summary>
        /// <param name="id">ID del formulario</param>
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
                var dto = new DeleteLogicalFormDto { Id = id, Status = status };
                var success = await _formBusiness.DeleteLogicFormAsync(dto);
                
                if (!success)
                {
                    return NotFound($"Formulario con ID {id} no encontrado");
                }

                return Ok($"Estado del formulario actualizado a {(status ? "activo" : "inactivo")}");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al cambiar estado del formulario {id}: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar estado del formulario {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
