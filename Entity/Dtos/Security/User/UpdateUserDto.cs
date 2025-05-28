using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.UserDTO
{
    /// <summary>
    /// DTO para actualización parcial de un usuario
    /// </summary>
    public class UpdateUserDto : BaseDto
    {
        /// <summary>
        /// Email del usuario (opcional para actualización parcial)
        /// </summary>
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; }

        /// <summary>
        /// ID de la persona asociada al usuario (opcional para actualización parcial)
        /// </summary>
        public int PersonId { get; set; }
    }
}
