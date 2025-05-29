using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para actualizar información de una ciudad (operación UPDATE)
    /// </summary>
    public class UpdateCityDto : BaseDto
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public int DepartmentId { get; set; }
    }
}
