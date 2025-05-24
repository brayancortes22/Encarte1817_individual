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
    /// DTO para actualizar información de un código postal (operación UPDATE)
    /// </summary>
    public class UpdateCodePostalDto : BaseDto
    {
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public int CityId { get; set; }
    }
}
