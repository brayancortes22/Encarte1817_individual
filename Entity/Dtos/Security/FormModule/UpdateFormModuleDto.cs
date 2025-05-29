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
    /// DTO para actualizar información de la relación Form-Module (operación UPDATE)
    /// </summary>
    public class UpdateFormModuleDto : BaseDto
    {
        public int FormId { get; set; }
        public int ModuleId { get; set; }
    }
}
