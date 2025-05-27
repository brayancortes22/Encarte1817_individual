using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.Module
{
    /// <summary>
    /// DTO para mostrar información básica de un módulo (operación GET ALL, CREATE)
    /// </summary>
    public class ModuleDto : BaseDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Icon { get; set; }
        
        public string Url { get; set; }
        public int Order { get; set; }
        
    }
}
