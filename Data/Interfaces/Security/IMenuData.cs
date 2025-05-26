using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IMenuData : IBaseModelData<Menu>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Menu menu);
        Task<List<Menu>> GetActiveAsync();
        Task<List<Menu>> GetMenusByRoleAsync(int roleId);
        Task<List<Menu>> GetMenusByParentIdAsync(int? parentId);
    }
}
