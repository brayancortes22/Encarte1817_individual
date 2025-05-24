using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Client
{
    /// <summary>
    /// DTO para mostrar información básica de un cliente (operación GET ALL, CREATE)
    /// </summary>
    public class ClientDto : BaseDto
    {
        public string ClientCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string ClientType { get; set; }
        public int PersonId { get; set; }
        
        // Propiedades adicionales para mostrar información relacionada
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
