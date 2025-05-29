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
    /// DTO para actualización parcial de un usuario
    /// </summary>
    public class UpdateUserDto : BaseDto
    {
        public string Email { get; set; }

        public int PersonId { get; set; }
    }
}
