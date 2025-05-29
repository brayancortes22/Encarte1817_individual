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
    /// DTO para actualizar información de la relación Module-Permission (operación UPDATE)
    /// </summary>
    public class UpdateModulePermissionDto : BaseDto
    {
        public int ModuleId { get; set; }
        public int PermissionId { get; set; }
    }
}
