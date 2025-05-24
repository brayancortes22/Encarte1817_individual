using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.Module
{
    /// <summary>
    /// DTO para la eliminación lógica de un módulo (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalModuleDto : BaseDto
    {
        public DeleteLogicalModuleDto()
        {
            Status = false;
        }
    }
}
