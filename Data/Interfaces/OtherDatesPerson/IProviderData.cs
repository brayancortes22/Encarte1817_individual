using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IProviderData : IBaseModelData<Provider>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Provider provider);
        Task<Provider> GetByDocumentNumberAsync(string documentNumber);
        Task<List<Provider>> GetActiveAsync();
        Task<List<Provider>> SearchProvidersByNameAsync(string name);
    }
}
