using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IClientData : IBaseModelData<Client>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Client client);
        Task<Client> GetByDocumentNumberAsync(string documentNumber);
        Task<List<Client>> GetActiveAsync();
        Task<List<Client>> SearchClientsByNameAsync(string name);
    }
}
