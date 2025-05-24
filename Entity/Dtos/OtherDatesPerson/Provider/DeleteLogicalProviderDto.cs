using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Provider
{
    /// <summary>
    /// DTO para la eliminación lógica de un proveedor (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalProviderDto : BaseDto
    {
        public DeleteLogicalProviderDto()
        {
            Status = false;
        }
    }
}
