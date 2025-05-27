using System;

namespace Entity.Model.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Status { get; set; } = true;
        
        // Solo mantenemos el estado activo/inactivo en la entidad principal
        // El resto de información de auditoría irá solo en la base de logs
    }
}
