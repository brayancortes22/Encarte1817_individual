using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        // Relaciones
        public ICollection<ModulePermission> ModulePermissions { get; set; }
        public ICollection<FormModule> FormModules { get; set; }
        
    }
}
