using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.ChangeLog
{
    /// <summary>
    /// DTO para la eliminación lógica de un registro de cambios (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalChangeLogDto : BaseDto
    {
        public DeleteLogicalChangeLogDto()
        {
            Status = false;
        }
    }
}
