using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.UserDTO;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserBusiness userBusiness, ILogger<UsersController> logger)
        {
            _userBusiness = userBusiness;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los usuarios activos del sistema
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                var users = await _userBusiness.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuarios: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios (activos e inactivos) del sistema
        /// </summary>
        /// <returns>Lista completa de usuarios</returns>
        [HttpGet("with-inactive")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllWithInactive()
        {
            try
            {
                var users = await _userBusiness.GetAllWithInactiveAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener todos los usuarios: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un usuario específico por su ID
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Usuario encontrado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userBusiness.GetByIdAsync(id);
                if (user == null)
                    return NotFound($"Usuario con ID {id} no encontrado");

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuario con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo usuario en el sistema
        /// </summary>
        /// <param name="userDto">Datos del usuario a crear</param>
        /// <returns>Usuario creado</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Create(UserDto userDto)
        {
            try
            {
                if (userDto == null)
                    return BadRequest("Los datos del usuario no pueden estar vacíos");

                var createdUser = await _userBusiness.CreateAsync(userDto);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear usuario: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza completamente un usuario existente
        /// </summary>
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="userDto">Datos actualizados del usuario</param>
        /// <returns>Usuario actualizado</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> Update(int id, UserDto userDto)
        {
            try
            {
                if (userDto == null || id != userDto.Id)
                    return BadRequest("Los datos del usuario son inválidos o el ID no coincide");

                var existingUser = await _userBusiness.GetByIdAsync(id);
                if (existingUser == null)
                    return NotFound($"Usuario con ID {id} no encontrado");

                var updatedUser = await _userBusiness.UpdateAsync(userDto);
                return Ok(updatedUser);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar usuario con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza parcialmente un usuario existente
        /// </summary>
        /// <param name="id">ID del usuario a actualizar</param>
        /// <param name="updateUserDto">Datos parciales a actualizar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePartial(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                if (updateUserDto == null || id != updateUserDto.Id)
                    return BadRequest("Los datos del usuario son inválidos o el ID no coincide");

                var existingUser = await _userBusiness.GetByIdAsync(id);
                if (existingUser == null)
                    return NotFound($"Usuario con ID {id} no encontrado");

                var result = await _userBusiness.UpdateParcialUserAsync(updateUserDto);
                if (result)
                    return Ok("Usuario actualizado correctamente");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente usuario con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina permanentemente un usuario del sistema
        /// </summary>
        /// <param name="id">ID del usuario a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingUser = await _userBusiness.GetByIdAsync(id);
                if (existingUser == null)
                    return NotFound($"Usuario con ID {id} no encontrado");

                var result = await _userBusiness.DeleteAsync(id);
                if (result)
                    return Ok("Usuario eliminado permanentemente");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el usuario");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar usuario con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina lógicamente un usuario (cambio de estado a inactivo)
        /// </summary>
        /// <param name="dto">Datos para eliminación lógica</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("set-status")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetActive(DeleteLogicalUserDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Los datos son inválidos");

                var existingUser = await _userBusiness.GetByIdAsync(dto.Id);
                if (existingUser == null)
                    return NotFound($"Usuario con ID {dto.Id} no encontrado");

                var result = await _userBusiness.SetUserActiveAsync(dto);
                if (result)
                    return Ok($"Estado del usuario actualizado a {(dto.Status ? "activo" : "inactivo")}");
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el estado del usuario");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar estado del usuario con ID {dto?.Id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        /// <param name="dto">Datos para el cambio de contraseña</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(UpdatePasswordUserDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Los datos son inválidos");

                var existingUser = await _userBusiness.GetByIdAsync(dto.Id);
                if (existingUser == null)
                    return NotFound($"Usuario con ID {dto.Id} no encontrado");

                var result = await _userBusiness.ChangePasswordAsync(dto);
                if (result)
                    return Ok("Contraseña actualizada correctamente");
                else
                    return BadRequest("La contraseña actual es incorrecta");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar contraseña del usuario con ID {dto?.Id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Asigna un rol a un usuario
        /// </summary>
        /// <param name="dto">Datos para la asignación de rol</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPost("assign-rol")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssignRol(AssignUserRolDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Los datos son inválidos");

                var result = await _userBusiness.AssignRolAsync(dto);
                if (result)
                    return Ok("Rol asignado correctamente");
                else
                    return BadRequest("No se pudo asignar el rol (usuario o rol no encontrado)");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al asignar rol: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }
    }
}
