using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Client : Base.BaseEntity
    {
        public string ClientCode { get; set; }
        public DateTime RegistrationDate { get; set; }// Fecha de registro
        public string ClientType { get; set; } // Individual or Corporate

        // Relación con Person (podría ser una referencia)
        public int PersonId { get; set; } // ID de la persona asociada al cliente
        public Person Person { get; set; }
    }
}