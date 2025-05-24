using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.CodePostal
{
    /// <summary>
    /// DTO para mostrar información básica de un código postal (operación GET ALL, CREATE)
    /// </summary>
    public class CodePostalDto : BaseDto
    {
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public int CityId { get; set; }
    }
}
