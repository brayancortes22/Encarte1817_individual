using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Country
{
    /// <summary>
    /// DTO para actualizar información de un país (operación UPDATE)
    /// </summary>
    public class UpdateCountryDto : BaseDto
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public string Currency { get; set; }

        public string PhonePrefix { get; set; }
        public int PersonId { get; set; }
    }
}
