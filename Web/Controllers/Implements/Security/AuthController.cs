using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.AuthDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Inicia sesión con credenciales de usuario
        /// </summary>
        /// <param name="credentials">Email y contraseña del usuario</param>
        /// <returns>Token JWT y fecha de expiración</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginDto credentials)
        {
            try
            {
                var result = await _authService.AuthenticateAsync(credentials.Email, credentials.Password);
                if (result == null)
                {
                    return Unauthorized("Credenciales inválidas");
                }

                return Ok(result);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning($"Error de autenticación: {ex.Message}");
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en login: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Solicita un token para restablecer contraseña
        /// </summary>
        /// <param name="request">Email del usuario</param>
        /// <returns>Mensaje de confirmación</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            try
            {
                var result = await _authService.RequestPasswordResetAsync(request.Email);
                if (!result)
                {
                    return NotFound("Usuario no encontrado");
                }

                return Ok(new { Message = "Se ha enviado un correo con instrucciones para restablecer la contraseña" });
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning($"Error en recuperación de contraseña: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en recuperación de contraseña: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Restablece la contraseña con un token válido
        /// </summary>
        /// <param name="request">Token y nueva contraseña</param>
        /// <returns>Mensaje de confirmación</returns>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            try
            {
                var result = await _authService.ResetPasswordAsync(request.Token, request.Password);
                if (!result)
                {
                    return BadRequest("Token inválido o expirado");
                }

                return Ok(new { Message = "Contraseña restablecida exitosamente" });
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning($"Error en restablecimiento de contraseña: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en restablecimiento de contraseña: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Verifica si un token de restablecimiento de contraseña es válido
        /// </summary>
        /// <param name="token">Token JWT a verificar</param>
        /// <returns>Estado de validez del token</returns>
        [HttpPost("verify-token")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult VerifyToken([FromBody] TokenVerificationDto tokenDto)
        {
            try
            {
                var isValid = _authService.ValidateToken(tokenDto.Token);
                return Ok(new { IsValid = isValid });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en verificación de token: {ex.Message}");
                return BadRequest("Token inválido");
            }
        }
    }
}
