using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces.Security;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utilities.Interfaces;
using Utilities.Mail;
using ValidationException = Utilities.Exceptions.ValidationException;
using Entity.Dtos;
namespace Business.Implements
{
    /// <summary>
    /// Implementación de la lógica de negocio para la gestión de usuarios.
    /// Extiende BaseBusiness heredando la lógica de negocio de los métodos base CRUD.
    /// </summary>
    public class UserBusiness : BaseBusiness<User, UserDto>, IUserBusiness
    {
        private readonly IUserData _userData;
        private readonly IEmailService _emailService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly AppSettings _appSettings;
        
        /// <summary>
        /// Constructor que inicializa las dependencias necesarias para la gestión de usuarios.
        /// </summary>
        public UserBusiness(
            IUserData userData,
            IMapper mapper,
            ILogger<UserBusiness> logger,
            IGenericIHelpers helpers,
            IEmailService emailService,
            IJwtGenerator jwtGenerator,
            IOptions<AppSettings> appSettings)
            : base(userData, mapper, logger, helpers)
        {
            _userData = userData;
            _emailService = emailService;
            _jwtGenerator = jwtGenerator;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Obtiene un usuario por su dirección de correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario a buscar.</param>
        /// <returns>El usuario que coincida con el correo proporcionado o null si no se encuentra.</returns>
        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("El email no puede estar vacío.");

                var user = await _userData.GetByEmailAsync(email);
                return user ?? throw new ValidationException("Usuario no encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar usuario por email: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Valida las credenciales de un usuario comparando el correo y la contraseña.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña en texto plano para validar.</param>
        /// <returns>True si las credenciales son válidas; de lo contrario false.</returns>
        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                    return false;

                // Hashear la contraseña para compararla con la almacenada
                var hashedPassword = HashPassword(password);
                var user = await _userData.LoginAsync(email, hashedPassword);
                
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al validar credenciales: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza parcialmente los datos de un usuario.
        /// </summary>
        /// <param name="dto">Objeto que contiene los datos parciales a actualizar del usuario.</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false.</returns>
        public async Task<bool> UpdateParcialUserAsync(UpdateUserDto dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    throw new ArgumentException("Los datos de actualización no son válidos.");

                // Crear un diccionario con las propiedades a actualizar
                var propertyValues = new Dictionary<string, object>();
                
                // Añadir al diccionario solo las propiedades no nulas
                if (!string.IsNullOrWhiteSpace(dto.Email))
                {
                    // Comprobar que el email no esté en uso por otro usuario
                    if (await _userData.ExistsByEmailAsync(dto.Email, dto.Id))
                        throw new ValidationException("El email ya está en uso por otro usuario.");
                        
                    propertyValues.Add("Email", dto.Email);
                }
                
                if (dto.PersonId > 0)
                    propertyValues.Add("PersonId", dto.PersonId);
                
                // Si no hay propiedades a actualizar, salir
                if (propertyValues.Count == 0)
                    return true;
                    
                // Actualizar parcialmente la entidad
                var updatedUser = await _data.UpdatePartialAsync(dto.Id, propertyValues);
                return updatedUser != null;
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar parcialmente el usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Cambia el estado activo/inactivo de un usuario.
        /// </summary>
        /// <param name="dto">Objeto que contiene el ID del usuario y el nuevo estado activo.</param>
        /// <returns>True si el cambio de estado fue exitoso; de lo contrario false.</returns>
        public async Task<bool> SetUserActiveAsync(DeleteLogicalUserDto dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    throw new ArgumentException("Los datos para cambiar el estado no son válidos.");
                    
                return await _userData.SetActiveStatusAsync(dto.Id, dto.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar el estado del usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Autentica un usuario en el sistema usando su correo electrónico y contraseña.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Objeto User si las credenciales son válidas; de lo contrario, null.</returns>
        public async Task<User> LoginAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("El email y la contraseña son obligatorios.");
                
                // Hashear la contraseña para compararla con la almacenada
                var hashedPassword = HashPassword(password);
                var user = await _userData.LoginAsync(email, hashedPassword);
                return user ?? throw new ValidationException("Credenciales incorrectas");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el proceso de login: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario existente.
        /// </summary>
        /// <param name="dto">Objeto que contiene el ID del usuario, la contraseña actual y la nueva contraseña.</param>
        /// <returns>True si la contraseña se cambió correctamente; false si el usuario no existe o la contraseña actual no coincide.</returns>
        public async Task<bool> ChangePasswordAsync(UpdatePasswordUserDto dto)
        {
            try
            {
                if (dto == null || dto.Id <= 0)
                    throw new ArgumentException("Los datos para cambiar la contraseña no son válidos.");
                
                // Obtener el usuario
                var user = await _data.GetByIdAsync(dto.Id);
                if (user == null)
                    return false;
                
                // Verificar la contraseña actual
                var hashedCurrentPassword = HashPassword(dto.CurrentPassword);
                if (user.Password != hashedCurrentPassword)
                    return false;
                
                // Actualizar con la nueva contraseña
                var hashedNewPassword = HashPassword(dto.NewPassword);
                return await _userData.ChangePasswordAsync(dto.Id, hashedNewPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar la contraseña: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Cambia la contraseña de un usuario por su ID y nueva contraseña.
        /// </summary>
        /// <param name="userId">ID del usuario.</param>
        /// <param name="newPassword">Nueva contraseña en texto plano.</param>
        /// <returns>True si la contraseña se cambió correctamente; false si el usuario no existe.</returns>
        public async Task<bool> ChangePasswordAsync(int userId, string newPassword)
        {
            try
            {
                if (userId <= 0 || string.IsNullOrWhiteSpace(newPassword))
                    throw new ArgumentException("Datos inválidos para cambiar la contraseña.");

                var user = await _data.GetByIdAsync(userId);
                if (user == null)
                    return false;

                var hashedNewPassword = HashPassword(newPassword);
                return await _userData.ChangePasswordAsync(userId, hashedNewPassword);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al cambiar la contraseña (por ID): {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Asigna un rol a un usuario específico.
        /// </summary>
        /// <param name="dto">Objeto que contiene el ID del usuario y el ID del rol a asignar.</param>
        /// <returns>True si el rol fue asignado correctamente; false si el usuario o el rol no existen.</returns>
        public async Task<bool> AssignRolAsync(AssignUserRolDto dto)
        {
            try
            {
                if (dto == null || dto.UserId <= 0 || dto.RolId <= 0)
                    throw new ArgumentException("Los datos para asignar el rol no son válidos.");
                
                return await _userData.AssingRolAsync(dto.UserId, dto.RolId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al asignar rol al usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Notifica al usuario mediante correo electrónico sobre la creación de su cuenta.
        /// </summary>
        /// <param name="emailDestino">Dirección de correo electrónico del destinatario.</param>
        /// <param name="nombre">Nombre del usuario para personalizar el mensaje.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        public async Task NotificarUsuarioAsync(string emailDestino, string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(emailDestino) || string.IsNullOrWhiteSpace(nombre))
                    throw new ArgumentException("El email y nombre son obligatorios para enviar la notificación.");
                
                var subject = "¡Bienvenido al sistema!";
                var message = $"Hola {nombre}, tu cuenta ha sido creada con éxito. Por favor, configura tu contraseña.";
                
                await _emailService.SendEmailAsync(emailDestino, subject, message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al notificar al usuario: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Envía un correo electrónico con un enlace para restablecer la contraseña del usuario.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico del usuario que solicita recuperar su contraseña.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        public async Task EnviarCorreoRecuperacionAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("El email es obligatorio para enviar el correo de recuperación.");
                
                // Comprobar que el usuario existe
                var user = await _userData.GetByEmailAsync(email);
                if (user == null)
                    throw new ValidationException("No existe un usuario con este correo electrónico.");
                
                // Generar token para restablecer contraseña
                var token = _jwtGenerator.GeneratePasswordResetToken(user);
                var resetUrl = $"{_appSettings.AppUrl}/reset-password?token={token}";
                
                var subject = "Recuperación de contraseña";
                var message = $"Para recuperar tu contraseña, haz clic en el siguiente enlace: {resetUrl}";
                
                await _emailService.SendEmailAsync(email, subject, message);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al enviar correo de recuperación: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Genera un hash SHA256 para una contraseña.
        /// </summary>
        /// <param name="password">Contraseña en texto plano.</param>
        /// <returns>Representación hash de la contraseña.</returns>
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}