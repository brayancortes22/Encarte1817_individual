using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Enums;

namespace Entity.Dtos.OtherDatesPerson.Employee
{
    /// <summary>
    /// DTO para mostrar información básica de un empleado (operación GET ALL, CREATE)
    /// </summary>
    public class EmployeeDto : BaseDto
    {
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime HiringDate { get; set; }
        public decimal Salary { get; set; }
        public ContractType ContractType { get; set; }
        public string EmployeeCode { get; set; }
        public string WorkEmail { get; set; }
        public int PersonId { get; set; }
    }
}
