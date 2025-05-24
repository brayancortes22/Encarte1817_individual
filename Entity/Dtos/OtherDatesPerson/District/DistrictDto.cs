using Entity.Dtos.Base;
using Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.District
{
    /// <summary>
    /// DTO para mostrar información básica de un distrito (operación GET ALL, CREATE)
    /// </summary>
    public class DistrictDto : BaseDto
    {
        public string DistrictName { get; set; }
        public StreetType StreetType { get; set; }
        public string StreetNumber { get; set; }
        public StreetLetter StreetLetter { get; set; }
        public string SecondaryNumber { get; set; }
        public StreetLetter SecondaryLetter { get; set; }
        public string TertiaryNumber { get; set; }
        public string AdditionalNumber { get; set; }
        public StreetLetter AdditionalLetter { get; set; }
        public int CityId { get; set; }
    }
}
