using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPersonBusiness : IBaseBusiness<Person, PersonDto>
    {
        // Métodos específicos para Person (si son necesarios)
    }
}
