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

        // Relación con departamentos/estados
        public Department Departments { get; set; }
    }
}