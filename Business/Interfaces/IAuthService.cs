using System.Security.Claims;
using System.Threading.Tasks;
using Entity.Dtos.AuthDTO;
using Entity.Dtos.CredencialesDTO;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos y operaciones de autenticación disponibles en el sistema.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Autentica a un usuario utilizando sus credenciales y genera un token JWT.
        /// </summary>
        /// <param name="credenciales">Objeto que contiene el correo electrónico y la contraseña del usuario.</param>
        /// <returns>Un objeto AuthDto que contiene el token JWT y su fecha de expiración.</returns>
        Task<AuthDto> LoginAsync(CredencialesDto credenciales);

        /// <summary>
        /// Autentica a un usuario utilizando su email y contraseña y genera un token JWT.
        /// </summary>
        /// <param name="email">El correo electrónico del usuario</param>
        /// <param name="password">La contraseña del usuario en texto plano</param>
        /// <returns>Un objeto AuthDto que contiene el token JWT y su fecha de expiración si las credenciales son válidas; de lo contrario, null.</returns>
        Task<AuthDto> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Envía un correo electrónico con instrucciones para restablecer la contraseña.
        /// </summary>
        /// <param name="email">El correo electrónico del usuario</param>
        /// <returns>True si el correo se envió correctamente; de lo contrario, false.</returns>
        Task<bool> RequestPasswordResetAsync(string email);

        /// <summary>
        /// Restablece la contraseña de un usuario utilizando un token válido.
        /// </summary>
        /// <param name="token">El token JWT de recuperación de contraseña</param>
        /// <param name="newPassword">La nueva contraseña del usuario</param>
        /// <returns>True si la contraseña se restableció correctamente; de lo contrario, false.</returns>
        Task<bool> ResetPasswordAsync(string token, string newPassword);

        /// <summary>
        /// Valida la autenticidad y vigencia de un token JWT.
        /// </summary>
        /// <param name="token">El token JWT a validar</param>
        /// <returns>True si el token es válido; de lo contrario, false.</returns>
        bool ValidateToken(string token);
    }
}