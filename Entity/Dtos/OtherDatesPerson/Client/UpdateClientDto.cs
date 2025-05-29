using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    /// <summary>
    /// DTO para actualizar información de un cliente (operación UPDATE)
    /// </summary>
    public class UpdateClientDto : BaseDto
    {
        public string ClientCode { get; set; }
        public DateTime RegistrationDate { get; set; }   
        public string ClientType { get; set; }
        public int PersonId { get; set; }
    }
}
