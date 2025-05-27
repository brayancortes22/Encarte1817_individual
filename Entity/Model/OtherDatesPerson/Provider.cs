using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Provider : Base.BaseEntity
    {
        public string CompanyName { get; set; }
        public string TaxId { get; set; } // NIT
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ServiceType { get; set; }

        // Relación con Person
        public int PersonId { get; set; } // ID de la persona asociada al proveedor
        public Person person { get; set; }
    }
}