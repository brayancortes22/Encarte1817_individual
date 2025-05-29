using Entity.Dtos.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para asignar un rol a un usuario
    /// </summary>
    public class AssignUserRolDto
    {
       
        public int UserId { get; set; }
        public int RolId { get; set; }
    }
}
