using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IModulePermissionData : IBaseModelData<ModulePermission>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(ModulePermission modulePermission);
        Task<List<ModulePermission>> GetActiveAsync();
        Task<List<ModulePermission>> GetByModuleIdAsync(int moduleId);
        Task<List<ModulePermission>> GetByPermissionIdAsync(int permissionId);
        Task<ModulePermission> GetByModulePermissionAsync(int moduleId, int permissionId);
    }
}
