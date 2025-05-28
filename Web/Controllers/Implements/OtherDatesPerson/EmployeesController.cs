using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.OtherDatesPerson.Employee;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeBusiness _employeeBusiness;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeBusiness employeeBusiness, ILogger<EmployeesController> logger)
        {
            _employeeBusiness = employeeBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los empleados activos
        /// </summary>
        /// <returns>Lista de empleados activos</returns>
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetActiveEmployees()
        {
            try
            {
                var employees = await _employeeBusiness.GetActiveEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener empleados activos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Busca un empleado por su número de documento
        /// </summary>
        /// <param name="documentNumber">Número de documento</param>
        /// <returns>Empleado encontrado</returns>
        [HttpGet("bydocument/{documentNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByDocument(string documentNumber)
        {
            try
            {
                var employee = await _employeeBusiness.GetEmployeeByDocumentNumberAsync(documentNumber);
                if (employee == null)
                    return NotFound($"Empleado con documento {documentNumber} no encontrado");
                
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar empleado por documento {documentNumber}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Busca empleados por su nombre
        /// </summary>
        /// <param name="name">Nombre o parte del nombre</param>
        /// <returns>Lista de empleados que coinciden con la búsqueda</returns>
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchEmployeesByName(string name)
        {
            try
            {
                var employees = await _employeeBusiness.SearchEmployeesByNameAsync(name);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar empleados por nombre {name}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene empleados por tipo de contrato
        /// </summary>
        /// <param name="contractTypeId">ID del tipo de contrato</param>
        /// <returns>Lista de empleados con el tipo de contrato especificado</returns>
        [HttpGet("bycontracttype/{contractTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByContractType(int contractTypeId)
        {
            try
            {
                var employees = await _employeeBusiness.GetEmployeesByContractTypeAsync(contractTypeId);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener empleados por tipo de contrato {contractTypeId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los empleados (activos e inactivos)
        /// </summary>
        /// <returns>Lista completa de empleados</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            try
            {
                var employees = await _employeeBusiness.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener empleados: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un empleado específico por su ID
        /// </summary>
        /// <param name="id">ID del empleado</param>
        /// <returns>Datos del empleado solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            try
            {
                var employee = await _employeeBusiness.GetByIdAsync(id);
                if (employee == null)
                    return NotFound($"Empleado con ID {id} no encontrado");
                
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener empleado con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo empleado en el sistema
        /// </summary>
        /// <param name="employeeDto">Datos del empleado a crear</param>
        /// <returns>Datos del empleado creado con su ID asignado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDto>> Create([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto == null)
                    return BadRequest("Los datos del empleado son inválidos");

                var createdEmployee = await _employeeBusiness.CreateAsync(employeeDto);
                return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al crear empleado: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear empleado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente los datos de un empleado existente
        /// </summary>
        /// <param name="id">ID del empleado a actualizar</param>
        /// <param name="employeeDto">Datos actualizados del empleado</param>
        /// <returns>Datos del empleado actualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EmployeeDto>> Update(int id, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto == null)
                    return BadRequest("Los datos del empleado son inválidos");

                if (id != employeeDto.Id)
                    return BadRequest("El ID en la URL no coincide con el ID en los datos");

                var existingEmployee = await _employeeBusiness.GetByIdAsync(id);
                if (existingEmployee == null)
                    return NotFound($"Empleado con ID {id} no encontrado");

                var updatedEmployee = await _employeeBusiness.UpdateAsync(employeeDto);
                return Ok(updatedEmployee);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar empleado: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar empleado con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un empleado
        /// </summary>
        /// <param name="updateEmployeeDto">Datos parciales para actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartial([FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            try
            {
                if (updateEmployeeDto == null)
                    return BadRequest("Los datos para actualización son inválidos");

                var success = await _employeeBusiness.UpdatePartialEmployeeAsync(updateEmployeeDto);
                if (!success)
                    return NotFound($"Empleado con ID {updateEmployeeDto.Id} no encontrado");

                return Ok("Empleado actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al actualizar parcialmente empleado: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente empleado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Realiza la eliminación lógica de un empleado (cambio de estado activo/inactivo)
        /// </summary>
        /// <param name="deleteDto">Datos para la eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("logical")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLogical([FromBody] DeleteLogicalEmployeeDto deleteDto)
        {
            try
            {
                if (deleteDto == null)
                    return BadRequest("Los datos para eliminación lógica son inválidos");

                var success = await _employeeBusiness.DeleteLogicEmployeeAsync(deleteDto);
                if (!success)
                    return NotFound($"Empleado con ID {deleteDto.Id} no encontrado");

                return Ok("Estado del empleado actualizado correctamente");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning($"Error de validación al eliminar lógicamente empleado: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar lógicamente empleado: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un empleado del sistema
        /// </summary>
        /// <param name="id">ID del empleado a eliminar</param>
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
                var success = await _employeeBusiness.DeleteAsync(id);
                if (!success)
                    return NotFound($"Empleado con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar empleado con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
