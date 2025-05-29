using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para el inicio de sesión de usuario
    /// </summary>
    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
