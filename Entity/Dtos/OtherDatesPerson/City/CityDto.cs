using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.OtherDatesPerson.City
{
    /// <summary>
    /// DTO para mostrar información básica de una ciudad (operación GET ALL, CREATE)
    /// </summary>
    public class CityDto : BaseDto
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public int DepartmentId { get; set; }
    }
}
