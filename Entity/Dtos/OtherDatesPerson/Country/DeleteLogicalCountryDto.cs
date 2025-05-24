using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Country
{
    /// <summary>
    /// DTO para la eliminación lógica de un país (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalCountryDto : BaseDto
    {
        public DeleteLogicalCountryDto()
        {
            Status = false;
        }
    }
}
