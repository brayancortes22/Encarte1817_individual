using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business.Interfaces.Security;
using Entity.Dtos;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : GenericController<User, UserDto>
    {
        private readonly IUserBusiness _userBusiness;

        /// <summary>
        /// Constructor del controlador de usuarios
        /// </summary>
        /// <param name="userBusiness">Servicio de negocio para usuarios</param>
        /// <param name="logger">Servicio de logging</param>
        public UserController(
            IUserBusiness userBusiness,
            ILogger<UserController> logger)
            : base(userBusiness, logger)
        {
            _userBusiness = userBusiness;
        }

        /// <summary>
        /// Obtiene un usuario por su dirección de email
        /// </summary>
        /// <param name="email">Email del usuario a buscar</param>
        /// <returns>Información del usuario encontrado</returns>
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await _userBusiness.GetByEmailAsync(email);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario por email");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Autenticar un usuario y generar un token JWT
        /// </summary>
        /// <param name="loginDto">Credenciales de login</param>
        /// <returns>Token JWT</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var token = await _userBusiness.AuthenticateAsync(loginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de autenticación");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualizar la contraseña de un usuario
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <param name="passwordDto">Datos de contraseña</param>
        /// <returns>Usuario actualizado</returns>
        [HttpPatch("{id}/password")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordUserDto passwordDto)
        {
            try
            {
                var updatedUser = await _userBusiness.UpdatePasswordAsync(id, passwordDto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la contraseña");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un usuario con todos sus detalles relacionados
        /// </summary>
        /// <param name="id">ID del usuario</param>
        /// <returns>Usuario con detalles</returns>
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserWithDetails(int id)
        {
            try
            {
                var user = await _userBusiness.GetUserWithDetailsAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalles del usuario");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
