using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{    public class Person : Base.BaseEntity
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TypeIdentification { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; } // Fecha de nacimiento
        public string Gender { get; set; } // Género
        public int? CountryId { get; set; } // ID del país
        public int? DepartmentId { get; set; } // ID del departamento
        public int? CityId { get; set; } // ID de la ciudad
        public int? DistrictId { get; set; } // ID del distrito
        public string PostalCode { get; set; } // Código postal
        public string ProfilePictureUrl { get; set; } // URL de la foto de perfil

        // Relaciones
        public Country Country { get; set; }
        public Department Department { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public User User { get; set; }
    }
}