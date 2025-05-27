using System;

namespace Entity.Model.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        
        // Campos de auditoría
        public DateTime? DeletedAt { get; set; }

    }
}
