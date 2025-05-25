using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.MenuPermissionData
{
    public class MenuPermissionData : BaseModelData<MenuPermission>, IMenuPermissionData
    {
        public MenuPermissionData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var menuPermission = await _context.Set<MenuPermission>().FindAsync(id);
            if (menuPermission == null)
                return false;

            menuPermission.Status = status;
            _context.Entry(menuPermission).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MenuPermission>> GetActiveAsync()
        {
            return await _context.Set<MenuPermission>()
                .Where(mp => mp.Status == true)
                .Include(mp => mp.Menu)
                .Include(mp => mp.Permission)
                .ToListAsync();
        }

        public async Task<List<MenuPermission>> GetByMenuIdAsync(int menuId)
        {
            return await _context.Set<MenuPermission>()
                .Where(mp => mp.Status == true && mp.MenuId == menuId)
                .Include(mp => mp.Menu)
                .Include(mp => mp.Permission)
                .ToListAsync();
        }

        public async Task<MenuPermission> GetByMenuPermissionAsync(int menuId, int permissionId)
        {
            return await _context.Set<MenuPermission>()
                .Where(mp => mp.Status == true && 
                       mp.MenuId == menuId && 
                       mp.PermissionId == permissionId)
                .Include(mp => mp.Menu)
                .Include(mp => mp.Permission)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MenuPermission>> GetByPermissionIdAsync(int permissionId)
        {
            return await _context.Set<MenuPermission>()
                .Where(mp => mp.Status == true && mp.PermissionId == permissionId)
                .Include(mp => mp.Menu)
                .Include(mp => mp.Permission)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(MenuPermission menuPermission)
        {
            var existingMenuPermission = await _context.Set<MenuPermission>().FindAsync(menuPermission.Id);
            if (existingMenuPermission == null) return false;
            
            // Actualizar solo si los IDs no son cero
            if (menuPermission.MenuId != 0)
                existingMenuPermission.MenuId = menuPermission.MenuId;
                
            if (menuPermission.PermissionId != 0)
                existingMenuPermission.PermissionId = menuPermission.PermissionId;
            
            _context.Set<MenuPermission>().Update(existingMenuPermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
