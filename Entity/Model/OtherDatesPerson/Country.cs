using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Country : Base.BaseEntity
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
        public string PhonePrefix { get; set; }
        public int PersonId { get; set; } // ID de la persona asociada al país
        // Relación con departamentos/estados
        public ICollection<Department> Departments { get; set; }
    }
}