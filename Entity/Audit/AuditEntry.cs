using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Entity.Audit
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
            KeyValues = new Dictionary<string, object>();
            OldValues = new Dictionary<string, object>();
            NewValues = new Dictionary<string, object>();
        }

        public EntityEntry Entry { get; }
        public string UserName { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public int IdTable { get; set; }
        public string EntityName { get; set; }
        public string IpAddress { get; set; }
        public Dictionary<string, object> KeyValues { get; }
        public Dictionary<string, object> OldValues { get; }
        public Dictionary<string, object> NewValues { get; }
    }
}