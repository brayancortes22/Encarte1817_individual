using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos.AuthDTO
{
    /// <summary>
    /// DTO para el inicio de sesión de usuario
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Correo electrónico del usuario
        /// </summary>
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = null!;
    }
}
