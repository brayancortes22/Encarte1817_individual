using Entity.Dtos.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos.OtherDatesPerson.Provider
{
    /// <summary>
    /// DTO para actualizar información de un proveedor (operación UPDATE)
    /// </summary>
    public class UpdateProviderDto : BaseDto
    {
        public string CompanyName { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ServiceType { get; set; }
        public int PersonId { get; set; }
    }
}
