using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class RolFormPermission : BaseEntity
    {
        // claves foraneas
        public int RolId { get; set; }
        public int PermissionId { get; set; }
        public int FormId { get; set; }
        
        // propiedades de navegación
        public Rol Rol { get; set; }
        public Form Form { get; set; }
        public Permission Permission { get; set; }
    }
}
