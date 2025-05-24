using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.RolFormPermission
{
    /// <summary>
    /// DTO para mostrar información básica de la relación Rol-Form-Permission (operación GET ALL, CREATE)
    /// </summary>
    public class RolFormPermissionDto : BaseDto
    {
        public int RolId { get; set; }

        public int PermissionId { get; set; }
        public int FormId { get; set; }
    }
}
