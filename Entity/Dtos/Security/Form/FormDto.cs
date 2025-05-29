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
    /// DTO para mostrar información básica de un formulario (operación GET ALL, CREATE)
    /// </summary>
    public class FormDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }
}
