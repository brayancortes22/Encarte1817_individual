using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.Permission
{
    /// <summary>
    /// DTO para la eliminación lógica de un permiso (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalPermissionDto : BaseDto
    {
        public DeleteLogicalPermissionDto()
        {
            Status = false;
        }
    }
}
