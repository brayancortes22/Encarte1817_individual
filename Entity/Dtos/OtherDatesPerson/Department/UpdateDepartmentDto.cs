using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Department
{
    /// <summary>
    /// DTO para actualizar información de un departamento/estado (operación UPDATE)
    /// </summary>
    public class UpdateDepartmentDto : BaseDto
    {

        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }

        public int CountryId { get; set; }
    }
}
