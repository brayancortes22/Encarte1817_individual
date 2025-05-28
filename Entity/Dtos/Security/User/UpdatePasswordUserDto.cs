using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.UserDTO
{
    /// <summary>
    /// DTO para actualizar la contraseña del usuario (operación UPDATE parcial)
    /// </summary>
    public class UpdatePasswordUserDto : BaseDto
    {
        /// <summary>
        /// Contraseña actual del usuario
        /// </summary>
        [Required(ErrorMessage = "La contraseña actual es obligatoria")]
        public string CurrentPassword { get; set; }
        
        /// <summary>
        /// Nueva contraseña del usuario
        /// </summary>
        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string NewPassword { get; set; }
    }
}
