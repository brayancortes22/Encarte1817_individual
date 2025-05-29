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
    /// DTO para mostrar información básica de la relación Module-Permission (operación GET ALL, CREATE)
    /// </summary>
    public class ModulePermissionDto : BaseDto
    {
        public int ModuleId { get; set; }
        public int PermissionId { get; set; }
    }
}
