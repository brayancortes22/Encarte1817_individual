using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRolFormPermissionData : IBaseModelData<RolFormPermission>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(RolFormPermission rolFormPermission);
        Task<List<RolFormPermission>> GetActiveAsync();
        Task<List<RolFormPermission>> GetByRolIdAsync(int rolId);
        Task<List<RolFormPermission>> GetByFormIdAsync(int formId);
        Task<List<RolFormPermission>> GetByPermissionIdAsync(int permissionId);
        Task<RolFormPermission> GetByRolFormPermissionAsync(int rolId, int formId, int permissionId);
    }
}
