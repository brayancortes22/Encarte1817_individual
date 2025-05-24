using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class CodePostal : Base.BaseEntity
    {
        public string postalCode { get; set; }
        public string area { get; set; } 

        // Relación con ciudad
        public City city { get; set; }
    }
}