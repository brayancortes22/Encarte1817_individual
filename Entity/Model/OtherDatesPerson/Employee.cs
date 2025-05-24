using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Enums;

namespace Entity.Model
{
    public class Employee : Base.BaseEntity
    {
        public int PersonId { get; set; } // Relación con Person
        public int UserId { get; set; } // Relación con User
        public string Position { get; set; }
        public string Department { get; set; }
        public DateTime HiringDate { get; set; } //fecha de contratación
        public decimal Salary { get; set; }
        public ContractType ContractType { get; set; } //tipo de contrato
        public string EmployeeCode { get; set; } // código de empleado
        public string WorkEmail { get; set; } // correo electrónico de trabajo

        // propiedad de navegacion
        public Person Person { get; set; }
        public User User { get; set; } // Relación con User
    }
}