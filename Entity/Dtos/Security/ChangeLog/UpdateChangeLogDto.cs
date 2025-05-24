using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.ChangeLog
{
    /// <summary>
    /// DTO para actualizar información de un registro de cambios (operación UPDATE)
    /// </summary>
    public class UpdateChangeLogDto : BaseDto
    {
        public int UserId { get; set; }

        public string Action { get; set; }

        public string EntityType { get; set; }

        public string EntityName { get; set; }

        public string EntityId { get; set; }
        
        public string OldValues { get; set; }
        
        public string NewValues { get; set; }
        
        public string IpAddress { get; set; }
        
        public string UserAgent { get; set; }
    }
}
