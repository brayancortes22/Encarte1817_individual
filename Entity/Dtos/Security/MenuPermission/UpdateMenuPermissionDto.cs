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
    /// DTO para actualizar información de la relación Menu-Permission (operación UPDATE)
    /// </summary>
    public class UpdateMenuPermissionDto : BaseDto
    {
        public int MenuId { get; set; }
        public int PermissionId { get; set; }
    }
}
