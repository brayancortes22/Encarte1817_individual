using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos.AuthDTO
{
    /// <summary>
    /// DTO para restablecer la contraseña con un token
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// Token JWT de restablecimiento de contraseña
        /// </summary>
        [Required(ErrorMessage = "El token es obligatorio")]
        public string Token { get; set; } = null!;

        /// <summary>
        /// Nueva contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
            ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una minúscula, un número y un carácter especial")]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Confirmación de la nueva contraseña
        /// </summary>
        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
