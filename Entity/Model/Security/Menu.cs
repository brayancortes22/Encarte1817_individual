using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public int? ParentMenuId { get; set; }
        public int ModuleId { get; set; }
        
        // Relaciones
        public Menu ParentMenu { get; set; }
        public ICollection<Menu> ChildMenus { get; set; }
        public Module Module { get; set; }
        public ICollection<MenuPermission> MenuPermissions { get; set; }
    }
}
