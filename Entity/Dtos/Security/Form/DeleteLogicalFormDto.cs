using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la eliminación lógica de un formulario (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalFormDto : BaseDto
    {
        public DeleteLogicalFormDto()
        {
            Status = false;
        }
    }
}
