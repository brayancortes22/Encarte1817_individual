using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.RolFormPermission
{
    /// <summary>
    /// DTO para la eliminación lógica de la relación Rol-Form-Permission (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalRolFormPermissionDto : BaseDto
    {
        public DeleteLogicalRolFormPermissionDto()
        {
            Status = false;
        }
    }
}
