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
    /// DTO para mostrar información básica de un proveedor (operación GET ALL, CREATE)
    /// </summary>
    public class ProviderDto : BaseDto
    {
        public string CompanyName { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ServiceType { get; set; }
        public int PersonId { get; set; }
    }
}
