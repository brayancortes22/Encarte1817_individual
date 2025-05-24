using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class MenuPermission : BaseEntity
    {
        public int MenuId { get; set; }
        public int PermissionId { get; set; }
        
        // Relaciones
        public Menu Menu { get; set; }
        public Permission Permission { get; set; }
    }
}
