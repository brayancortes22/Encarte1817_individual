using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IPermissionData : IBaseModelData<Permission>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Permission permission);
        Task<List<Permission>> GetByTypeAsync(int permissionTypeId);
        Task<List<Permission>> GetActiveAsync();
    }
}
