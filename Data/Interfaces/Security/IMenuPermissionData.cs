using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IMenuPermissionData : IBaseModelData<MenuPermission>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(MenuPermission menuPermission);
        Task<List<MenuPermission>> GetActiveAsync();
        Task<List<MenuPermission>> GetByMenuIdAsync(int menuId);
        Task<List<MenuPermission>> GetByPermissionIdAsync(int permissionId);
        Task<MenuPermission> GetByMenuPermissionAsync(int menuId, int permissionId);
    }
}
