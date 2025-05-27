using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;
using Entity.Enums;

namespace Entity.Dtos.Security.Permission
{
    /// <summary>
    /// DTO para mostrar información básica de un permiso (operación GET ALL, CREATE)
    /// </summary>
    public class PermissionDto : BaseDto
    {

        public string Name { get; set; }
        
        public string Description { get; set; }
        public string PermissionCode { get; set; }
        public PermissionType Type { get; set; }
        
        public int ModuleId { get; set; }
    }
}
