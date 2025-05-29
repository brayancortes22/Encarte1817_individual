using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para la eliminación lógica de un distrito (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalDistrictDto : BaseDto
    {
        public DeleteLogicalDistrictDto()
        {
            Status = false;
        }
    }
}
