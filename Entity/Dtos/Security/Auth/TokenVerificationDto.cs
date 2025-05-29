using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para verificar la validez de un token
    /// </summary>
    public class TokenVerificationDto
    {
        /// <summary>
        /// Token JWT a verificar
        /// </summary>
        [Required(ErrorMessage = "El token es obligatorio")]
        public string Token { get; set; } = null!;
    }
}
