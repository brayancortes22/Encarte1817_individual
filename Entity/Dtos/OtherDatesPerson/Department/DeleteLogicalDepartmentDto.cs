using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la eliminación lógica de un departamento/estado (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalDepartmentDto : BaseDto
    {
        public DeleteLogicalDepartmentDto()
        {
            Status = false;
        }
    }
}
