using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.MenuData
{
    public class MenuData : BaseModelData<Menu>, IMenuData
    {
        public MenuData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var menu = await _context.Set<Menu>().FindAsync(id);
            if (menu == null)
                return false;

            menu.Status = status;
            _context.Entry(menu).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Menu>> GetActiveAsync()
        {
            return await _context.Set<Menu>()
                .Where(m => m.Status == true)
                .Include(m => m.ParentMenu)
                .Include(m => m.ChildMenus.Where(cm => cm.Status == true))
                .Include(m => m.Module)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetMenusByParentIdAsync(int? parentId)
        {
            return await _context.Set<Menu>()
                .Where(m => m.Status == true && m.ParentMenuId == parentId)
                .Include(m => m.ChildMenus.Where(cm => cm.Status == true))
                .Include(m => m.Module)
                .ToListAsync();
        }

        public async Task<List<Menu>> GetMenusByRoleAsync(int roleId)
        {
            return await _context.Set<Menu>()
                .Where(m => m.Status == true && 
                           m.MenuPermissions.Any(mp => 
                               mp.Permission.RolFormPermissions.Any(rfp => 
                                   rfp.Rol.Id == roleId)))
                .Include(m => m.ParentMenu)
                .Include(m => m.Module)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Menu menu)
        {
            var existingMenu = await _context.Set<Menu>().FindAsync(menu.Id);
            if (existingMenu == null) return false;
            
            // Actualizar solo los campos necesarios
            if (menu.Name != null)
                existingMenu.Name = menu.Name;
                
            if (menu.Description != null)
                existingMenu.Description = menu.Description;
                
            if (menu.Icon != null)
                existingMenu.Icon = menu.Icon;
                
            if (menu.Url != null)
                existingMenu.Url = menu.Url;
                
            // Actualizar valores numéricos si no son cero
            if (menu.Order != 0)
                existingMenu.Order = menu.Order;
                
            if (menu.ModuleId != 0)
                existingMenu.ModuleId = menu.ModuleId;
                
            // Actualizar ID del menú padre si es diferente de nulo o cero
            if (menu.ParentMenuId.HasValue && menu.ParentMenuId.Value != 0)
                existingMenu.ParentMenuId = menu.ParentMenuId;
                
            // Si se proporciona un valor nulo para ParentMenuId, establecerlo como nulo
            if (!menu.ParentMenuId.HasValue)
                existingMenu.ParentMenuId = null;
            
            _context.Set<Menu>().Update(existingMenu);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
