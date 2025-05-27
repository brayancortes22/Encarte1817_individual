using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;
using Entity.Enums;

namespace Entity.Model
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PermissionCode { get; set; }  // Código único para el permiso
        public PermissionType Type { get; set; }    // Tipo de permiso (CRUD)  
              
        // propiedades de navegación
        public ICollection<ModulePermission> ModulePermissions { get; set; }
        public ICollection<RolFormPermission> RolFormPermissions { get; set; }
        
    }
}
