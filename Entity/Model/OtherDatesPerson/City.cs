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
        

        // Relaciones
        public Department Department { get; set; }
        public List<District> Districts { get; set; }
        public List<CodePostal> PostalCodes { get; set; }
    }
}