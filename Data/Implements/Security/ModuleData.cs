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
    public class ModuleData : BaseModelData<Module>, IModuleData
    {
        public ModuleData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var module = await _context.Set<Module>().FindAsync(id);
            if (module == null)
                return false;

            module.Status = status;
            _context.Entry(module).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Module>> GetActiveAsync()
        {
            return await _context.Set<Module>()
                .Where(m => m.Status == true)
                .Include(m => m.ModulePermissions)
                .ToListAsync();
        }

        public async Task<List<Module>> GetModulesByRoleAsync(int roleId)
        {
            return await _context.Set<Module>()
                .Where(m => m.Status == true && 
                           m.ModulePermissions.Any(mp => 
                               mp.Permission.RolFormPermissions.Any(rfp => 
                                   rfp.Rol.Id == roleId)))
                .Include(m => m.ModulePermissions)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Module module)
        {
            var existingModule = await _context.Set<Module>().FindAsync(module.Id);
            if (existingModule == null) return false;
            
            // Actualizar solo los campos necesarios
            if (module.Name != null)
                existingModule.Name = module.Name;
                
            if (module.Description != null)
                existingModule.Description = module.Description;
                
            if (module.Icon != null)
                existingModule.Icon = module.Icon;
                
            if (module.Url != null)
                existingModule.Url = module.Url;
                
            // Actualizar valores numéricos si no son cero
            if (module.Order != 0)
                existingModule.Order = module.Order;

            _context.Set<Module>().Update(existingModule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
