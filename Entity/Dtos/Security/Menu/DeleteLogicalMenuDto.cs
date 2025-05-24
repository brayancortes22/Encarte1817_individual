using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.Menu
{
    /// <summary>
    /// DTO para la eliminación lógica de un menú (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalMenuDto : BaseDto
    {
        public DeleteLogicalMenuDto()
        {
            Status = false;
        }
    }
}
