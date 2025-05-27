using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class ChangeLog
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int IdTable { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string OldValues { get; set; } = string.Empty;
        public string NewValues { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // Insert, Update, Delete
        public bool Active { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string EntityName { get; set; } = string.Empty; // Nombre de la clase de la entidad
        public string IpAddress { get; set; } = string.Empty; // Dirección IP del usuario
    }
}
