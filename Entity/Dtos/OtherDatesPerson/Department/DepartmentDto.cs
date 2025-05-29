using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para mostrar información básica de un departamento/estado (operación GET ALL, CREATE)
    /// </summary>
    public class DepartmentDto : BaseDto
    {
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public int CountryId { get; set; }
    }
}
