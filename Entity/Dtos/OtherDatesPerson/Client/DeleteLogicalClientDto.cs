using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Client
{
    /// <summary>
    /// DTO para la eliminación lógica de un cliente (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalClientDto : BaseDto
    {
        public DeleteLogicalClientDto()
        {
            Status = false;
        }
    }
}
