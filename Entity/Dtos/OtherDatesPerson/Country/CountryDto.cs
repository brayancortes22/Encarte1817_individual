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
    /// DTO para mostrar información básica de un país (operación GET ALL, CREATE)
    /// </summary>
    public class CountryDto : BaseDto
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Currency { get; set; }
        public string PhonePrefix { get; set; }
        public int PersonId { get; set; }
    }
}
