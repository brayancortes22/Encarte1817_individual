using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IModuleData : IBaseModelData<Module>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Module module);
        Task<List<Module>> GetActiveAsync();
        Task<List<Module>> GetModulesByRoleAsync(int roleId);
    }
}
