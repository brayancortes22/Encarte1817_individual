using Entity.Dtos.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos.UserDTO
{
    /// <summary>
    /// DTO para asignar un rol a un usuario
    /// </summary>
    public class AssignUserRolDto
    {
        /// <summary>
        /// ID del usuario al que se le asignará el rol
        /// </summary>
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public int UserId { get; set; }

        /// <summary>
        /// ID del rol que se asignará
        /// </summary>
        [Required(ErrorMessage = "El ID del rol es obligatorio")]
        public int RolId { get; set; }
    }
}
