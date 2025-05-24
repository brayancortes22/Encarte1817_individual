using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.MenuPermission
{
    /// <summary>
    /// DTO para mostrar información básica de la relación Menu-Permission (operación GET ALL, CREATE)
    /// </summary>
    public class MenuPermissionDto : BaseDto
    {
        public int MenuId { get; set; }
        public int PermissionId { get; set; }
    }
}
