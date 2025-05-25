using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IFormModuleData : IBaseModelData<FormModule>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(FormModule formModule);
        Task<List<FormModule>> GetActiveAsync();
        Task<List<FormModule>> GetFormModulesByFormIdAsync(int formId);
        Task<List<FormModule>> GetFormModulesByModuleIdAsync(int moduleId);
    }
}
