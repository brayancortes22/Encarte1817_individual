using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPersonData : IBaseModelData<Person>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Person person);
        Task<Person> GetByDocumentNumberAsync(string documentNumber);
        Task<List<Person>> SearchPersonsByNameAsync(string name);
    }
}
