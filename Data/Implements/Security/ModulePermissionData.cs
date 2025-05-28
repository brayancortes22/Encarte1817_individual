using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements
{
    public class ModulePermissionData : BaseModelData<ModulePermission>, IModulePermissionData
    {
        public ModulePermissionData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var modulePermission = await _context.Set<ModulePermission>().FindAsync(id);
            if (modulePermission == null)
                return false;

            modulePermission.Status = status;
            _context.Entry(modulePermission).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ModulePermission>> GetActiveAsync()
        {
            return await _context.Set<ModulePermission>()
                .Where(mp => mp.Status == true)
                .Include(mp => mp.Module)
                .Include(mp => mp.Permission)
                .ToListAsync();
        }

        public async Task<List<ModulePermission>> GetByModuleIdAsync(int moduleId)
        {
            return await _context.Set<ModulePermission>()
                .Where(mp => mp.Status == true && mp.ModuleId == moduleId)
                .Include(mp => mp.Module)
                .Include(mp => mp.Permission)
                .ToListAsync();
        }

        public async Task<ModulePermission> GetByModulePermissionAsync(int moduleId, int permissionId)
        {
            return await _context.Set<ModulePermission>()
                .Where(mp => mp.Status == true && 
                       mp.ModuleId == moduleId && 
                       mp.PermissionId == permissionId)
                .Include(mp => mp.Module)
                .Include(mp => mp.Permission)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ModulePermission>> GetByPermissionIdAsync(int permissionId)
        {
            return await _context.Set<ModulePermission>()
                .Where(mp => mp.Status == true && mp.PermissionId == permissionId)
                .Include(mp => mp.Module)
                .Include(mp => mp.Permission)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(ModulePermission modulePermission)
        {
            var existingModulePermission = await _context.Set<ModulePermission>().FindAsync(modulePermission.Id);
            if (existingModulePermission == null) return false;
            
            // Actualizar solo si los IDs no son cero
            if (modulePermission.ModuleId != 0)
                existingModulePermission.ModuleId = modulePermission.ModuleId;
                
            if (modulePermission.PermissionId != 0)
                existingModulePermission.PermissionId = modulePermission.PermissionId;
            
            _context.Set<ModulePermission>().Update(existingModulePermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
