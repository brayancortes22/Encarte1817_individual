using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Entity.Dtos.Base;

namespace Entity.Dtos.Security.Person
{
    /// <summary>
    /// DTO para actualizar información de una persona (operación UPDATE)
    /// </summary>
    public class UpdatePersonDto : BaseDto
    {

        public string Name { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string TypeIdentification { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public string Gender { get; set; }
        
        public int? CountryId { get; set; }
        
        public int? DepartmentId { get; set; }
        
        public int? CityId { get; set; }
        
        public int? DistrictId { get; set; }
        
        public string PostalCode { get; set; }
        
        public string ProfilePictureUrl { get; set; }
    }
}
