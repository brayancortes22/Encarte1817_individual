using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.MenuPermission
{
    /// <summary>
    /// DTO para la eliminación lógica de la relación Menu-Permission (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalMenuPermissionDto : BaseDto
    {
        public DeleteLogicalMenuPermissionDto()
        {
            Status = false;
        }
    }
}
