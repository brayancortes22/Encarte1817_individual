using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class FormModule : BaseEntity
    {
        public int FormId { get; set; }
        public int ModuleId { get; set; }

        public Form Form { get; set; }
        public Module Module { get; set; }
    }
}
