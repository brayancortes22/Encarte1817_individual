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
    /// DTO para actualizar información de un empleado (operación UPDATE)
    /// </summary>
    public class UpdateEmployeeDto : BaseDto
    {
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime HiringDate { get; set; }
        public decimal Salary { get; set; }
        public string ContractType { get; set; }
        public string EmployeeCode { get; set; }
        public string WorkEmail { get; set; }
        public int PersonId { get; set; }
    }
}
