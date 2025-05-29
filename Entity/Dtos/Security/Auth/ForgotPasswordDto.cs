using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para solicitar la recuperación de contraseña
    /// </summary>
    public class ForgotPasswordDto
    {
        public string Email { get; set; } = null!;
    }
}
