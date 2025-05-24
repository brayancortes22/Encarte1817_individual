using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Employee : Base.BaseEntity
    {
      
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime HiringDate { get; set; } //fecha de contratación
        public decimal Salary { get; set; }
        public string ContractType { get; set; } //tipo de contrato
        public string EmployeeCode { get; set; } // código de empleado
        public string WorkEmail { get; set; } // correo electrónico de trabajo
        
        // Relación con Person
        public Person Person { get; set; }
    }
}