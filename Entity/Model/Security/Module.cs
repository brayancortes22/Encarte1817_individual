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
        public int? ParentModuleId { get; set; } // Nullable para permitir módulos raíz sin padre
        
        // Relaciones
        public ICollection<ModulePermission> ModulePermissions { get; set; }
    }
}
