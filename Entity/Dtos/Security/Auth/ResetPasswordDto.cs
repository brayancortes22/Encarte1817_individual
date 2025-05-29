using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para restablecer la contraseña con un token
    /// </summary>
    public class ResetPasswordDto
    {
        public string Token { get; set; } = null!;

        public string Password { get; set; } = null!;

         public string ConfirmPassword { get; set; } = null!;
    }
}
