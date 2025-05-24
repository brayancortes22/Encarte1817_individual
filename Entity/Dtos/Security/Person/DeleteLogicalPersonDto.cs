using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.Person
{
    /// <summary>
    /// DTO para la eliminación lógica de una persona (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalPersonDto : BaseDto
    {
        public DeleteLogicalPersonDto()
        {
            Status = false;
        }
    }
}
