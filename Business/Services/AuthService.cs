using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Interfaces;
using Entity.Dtos.AuthDTO;
using Entity.Dtos.CredencialesDTO;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utilities.Exceptions;
using Utilities.Interfaces;
using Utilities.Mail;

namespace Business.Services
{
    /// <summary>
    /// Implementación del servicio de autenticación que gestiona el proceso de login
    /// y obtención de tokens JWT.
    /// </summary>
    public class AuthService : IAuthService
    {        private readonly IUserBusiness _userBusiness;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IEmailService _emailService;
        private readonly Utilities.AppSettings _appSettings;
        private readonly IPasswordHelper _passwordHelper;

        /// <summary>
        /// Inicializa una nueva instancia del servicio de autenticación.
        /// </summary>
        /// <param name="userBusiness">Servicio para operaciones de negocio relacionadas con usuarios.</param>
        /// <param name="jwtService">Servicio para la generación y validación de tokens JWT.</param>
        /// <param name="logger">Servicio de logging para registrar eventos y errores.</param>
        /// <param name="jwtGenerator">Generador de tokens JWT.</param>
        /// <param name="emailService">Servicio para envío de correos electrónicos.</param>
        /// <param name="appSettings">Configuración global de la aplicación.</param>
        /// <param name="passwordHelper">Servicio para gestión de contraseñas.</param>
        public AuthService(
            IUserBusiness userBusiness,
            IJwtService jwtService,
            ILogger<AuthService> logger,
            IJwtGenerator jwtGenerator,
            IEmailService emailService,
            IOptions<Utilities.AppSettings> appSettings,
            IPasswordHelper passwordHelper)
        {
            _userBusiness = userBusiness;
            _jwtService = jwtService;
            _logger = logger;
            _jwtGenerator = jwtGenerator;
            _emailService = emailService;
            _appSettings = appSettings.Value;
            _passwordHelper = passwordHelper;
        }

        /// <summary>
        /// Autentica a un usuario utilizando sus credenciales y genera un token JWT.
        /// </summary>
        /// <param name="credenciales">Credenciales del usuario (email y contraseña).</param>
        /// <returns>Un objeto AuthDto que contiene el token JWT y su fecha de expiración.</returns>
        /// <exception cref="UnauthorizedAccessException">Se lanza cuando las credenciales proporcionadas son inválidas.</exception>
        /// <exception cref="Exception">Se lanza cuando ocurre un error inesperado durante el proceso de autenticación.</exception>
        public async Task<AuthDto> LoginAsync(CredencialesDto credenciales)
        {
            try
            {
                // Verificar las credenciales del usuario
                var user = await _userBusiness.LoginAsync(credenciales.Email, credenciales.Password);

                // Si no se encontró el usuario o las credenciales son incorrectas
                if (user == null)
                    throw new UnauthorizedAccessException("Credenciales inválidas");

                // Generar y devolver el token JWT
                return await _jwtService.GenerateTokenAsync(user);
            }
            catch (Exception ex)
            {
                // Registrar el error para diagnóstico
                _logger.LogError($"Error durante el login: {ex.Message}");

                // Relanzar la excepción para que se maneje en capas superiores
                throw;
            }
        }

        /// <summary>
        /// Autentica un usuario por email y contraseña, generando un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <param name="password">Contraseña en texto plano</param>
        /// <returns>Objeto AuthDto con el token si las credenciales son válidas; null en caso contrario</returns>
        public async Task<AuthDto> AuthenticateAsync(string email, string password)
        {
            try
            {
                // Verificar si el usuario existe y las credenciales son correctas
                var isValid = await _userBusiness.ValidateCredentialsAsync(email, password);
                if (!isValid)
                {
                    _logger.LogWarning($"Intento de acceso fallido para email: {email}");
                    return null;
                }

                // Obtener el usuario para generar el token
                var user = await _userBusiness.GetByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogError($"Usuario validado pero no encontrado para email: {email}");
                    throw new BusinessException("Error en la autenticación");
                }

                // Generar el token JWT
                return await _jwtGenerator.GeneradorToken(user);
            }
            catch (BusinessException)
            {
                throw; // Dejamos que las excepciones de negocio se propaguen
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error durante la autenticación: {ex.Message}");
                throw new BusinessException("Error en el servicio de autenticación");
            }
        }

        /// <summary>
        /// Envía un correo con un token para restablecer la contraseña
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <returns>True si se envió el correo; false si el usuario no existe</returns>
        public async Task<bool> RequestPasswordResetAsync(string email)
        {
            try
            {
                // Verificar si el usuario existe
                var user = await _userBusiness.GetByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogWarning($"Intento de recuperación para email inexistente: {email}");
                    return false;
                }

                // Generar token para restablecimiento de contraseña
                var token = _jwtGenerator.GeneratePasswordResetToken(user);

                // Construir URL de restablecimiento
                var resetUrl = $"{_appSettings.ResetPasswordBaseUrl}?token={token}";

                // Construir el mensaje
                var subject = "Restablecimiento de Contraseña";
                var body = $@"
                    <h2>Restablecimiento de Contraseña</h2>
                    <p>Hemos recibido una solicitud para restablecer su contraseña.</p>
                    <p>Por favor, haga clic en el siguiente enlace para crear una nueva contraseña:</p>
                    <p><a href='{resetUrl}'>Restablecer mi contraseña</a></p>
                    <p>Si usted no solicitó este cambio, puede ignorar este mensaje.</p>
                    <p>Este enlace expirará en 30 minutos por razones de seguridad.</p>
                ";

                // Enviar correo electrónico
                await _emailService.SendEmailAsync(email, subject, body);
                
                _logger.LogInformation($"Correo de restablecimiento enviado a: {email}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al solicitar restablecimiento de contraseña: {ex.Message}");
                throw new BusinessException("Error al procesar la solicitud de restablecimiento");
            }
        }

        /// <summary>
        /// Restablece la contraseña de un usuario usando un token válido
        /// </summary>
        /// <param name="token">Token JWT de restablecimiento</param>
        /// <param name="newPassword">Nueva contraseña</param>
        /// <returns>True si se cambió la contraseña; false si el token es inválido</returns>
        public async Task<bool> ResetPasswordAsync(string token, string newPassword)
        {
            try
            {
                // Validar el token
                var principal = _jwtGenerator.ValidateToken(token);
                if (principal == null)
                {
                    _logger.LogWarning("Intento de restablecimiento con token inválido");
                    return false;
                }

                // Extraer el ID de usuario y verificar el tipo de token
                var userIdClaim = principal.FindFirst("id")?.Value;
                var tokenTypeClaim = principal.FindFirst("tipo")?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || tokenTypeClaim != "password_reset")
                {
                    _logger.LogWarning("Token de tipo incorrecto o sin ID de usuario");
                    return false;
                }

                // Convertir el ID a entero
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    _logger.LogError("ID de usuario inválido en el token");
                    return false;
                }

                // Validar la complejidad de la contraseña (esto ya lo hace el DTO con anotaciones)
                
                // Hashear la nueva contraseña
                var hashedPassword = _passwordHelper.HashPassword(newPassword);

                // Obtener el usuario para el email (si es necesario)
                var user = await _userBusiness.GetByIdAsync(userId);

                // Actualizar la contraseña (ajusta los argumentos según la definición real)
                var success = await _userBusiness.ChangePasswordAsync(userId, hashedPassword);

                if (success)
                {
                    _logger.LogInformation($"Contraseña restablecida exitosamente para el usuario ID: {userId}");
                }
                else
                {
                    _logger.LogWarning($"Error al restablecer la contraseña para el usuario ID: {userId}");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al restablecer contraseña: {ex.Message}");
                throw new BusinessException("Error al procesar el restablecimiento de contraseña");
            }
        }

        /// <summary>
        /// Verifica si un token JWT es válido
        /// </summary>
        /// <param name="token">Token JWT a validar</param>
        /// <returns>True si el token es válido; false en caso contrario</returns>
        public bool ValidateToken(string token)
        {
            try
            {
                var principal = _jwtGenerator.ValidateToken(token);
                return principal != null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error al validar token: {ex.Message}");
                return false;
            }
        }
    }
}