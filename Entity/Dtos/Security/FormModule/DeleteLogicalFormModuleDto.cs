using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la eliminación lógica de la relación Form-Module (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalFormModuleDto : BaseDto
    {
        public DeleteLogicalFormModuleDto()
        {
            Status = false;
        }
    }
}
