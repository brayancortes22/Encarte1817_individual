using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class ChangeLog : BaseEntity
    {
        public int UserId { get; set; }
        public string Action { get; set; } // Create, Update, Delete
        public string EntityType { get; set; } // Module, Form, etc.
        public string EntityName { get; set; } // Name of the entity (e.g., Module, Form)
        public string EntityId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; } 
        
    }
}
