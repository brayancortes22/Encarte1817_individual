using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Employee
{
    /// <summary>
    /// DTO para la eliminación lógica de un empleado (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalEmployeeDto : BaseDto
    {
        public DeleteLogicalEmployeeDto()
        {
            Status = false;
        }
    }
}
