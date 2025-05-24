using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.FormModule
{
    /// <summary>
    /// DTO para mostrar información básica de la relación Form-Module (operación GET ALL, CREATE)
    /// </summary>
    public class FormModuleDto : BaseDto
    {
        public int FormId { get; set; }
        public int ModuleId { get; set; }
    }
}
