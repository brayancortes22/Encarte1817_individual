using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class City : Base.BaseEntity
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public int CodePostal { get; set; }
        public int DepartmentId { get; set; }

        // Relaciones
        public Department Department { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}