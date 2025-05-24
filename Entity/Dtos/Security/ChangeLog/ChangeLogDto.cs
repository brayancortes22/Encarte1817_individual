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
    /// DTO para mostrar información básica de un registro de cambios (operación GET ALL, CREATE)
    /// </summary>
    public class ChangeLogDto : BaseDto
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
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
