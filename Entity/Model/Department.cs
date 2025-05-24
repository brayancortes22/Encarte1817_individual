using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Department : Base.BaseEntity
    {
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }

        // Relaciones
        public Country Country { get; set; }
        public List<City> Cities { get; set; }
    }
}