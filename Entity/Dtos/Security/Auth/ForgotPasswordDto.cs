using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos.AuthDTO
{
    /// <summary>
    /// DTO para solicitar la recuperación de contraseña
    /// </summary>
    public class ForgotPasswordDto
    {
        /// <summary>
        /// Correo electrónico del usuario que olvidó su contraseña
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string Email { get; set; } = null!;
    }
}
