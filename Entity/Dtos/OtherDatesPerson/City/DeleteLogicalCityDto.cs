using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Dtos.Base;

namespace Entity.Dtos.OtherDatesPerson.City
{
    /// <summary>
    /// DTO para la eliminación lógica de una ciudad (operación DELETE lógico)
    /// </summary>
    public class DeleteLogicalCityDto : BaseDto
    {
        public DeleteLogicalCityDto()
        {
            Status = false;
        }
    }
}
