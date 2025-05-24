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
    /// DTO para actualizar información de un distrito (operación UPDATE)
    /// </summary>
    public class UpdateDistrictDto : BaseDto
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
