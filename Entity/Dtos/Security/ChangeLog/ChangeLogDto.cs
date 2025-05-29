using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para mostrar información básica de un registro de cambios (operación GET ALL, CREATE)
    /// </summary>
    public class ChangeLogDto : BaseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int IdTable { get; set; }
        public string TableName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string Action { get; set; }
        public string Active { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
