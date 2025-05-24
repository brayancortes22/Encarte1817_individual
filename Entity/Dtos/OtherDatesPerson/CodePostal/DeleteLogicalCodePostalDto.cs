using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.CodePostal
{
    /// <summary>
    /// DTO para la eliminación lógica de un código postal (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalCodePostalDto : BaseDto
    {
        public DeleteLogicalCodePostalDto()
        {
            Status = false;
        }
    }
}
