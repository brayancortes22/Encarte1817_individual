using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.Base;

namespace Entity.Model
{
    public class User : BaseEntity
    {
        public  string Email { get; set; }
        public string Password { get; set; }

        //relación con Person
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public ICollection<RolUser> RolUsers { get; set; }
    }
}
