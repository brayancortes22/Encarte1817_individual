using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class ModulePermission : BaseEntity
    {
        public int ModuleId { get; set; }
        public int PermissionId { get; set; }
        
        // Relaciones
        public Module Module { get; set; }
        public Permission Permission { get; set; }
    }
}
