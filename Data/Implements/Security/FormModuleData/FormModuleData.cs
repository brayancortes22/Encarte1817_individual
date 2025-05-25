using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.FormModuleData
{
    public class FormModuleData : BaseModelData<FormModule>, IFormModuleData
    {
        public FormModuleData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var formModule = await _context.Set<FormModule>().FindAsync(id);
            if (formModule == null)
                return false;

            formModule.Status = status;
            _context.Entry(formModule).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FormModule>> GetActiveAsync()
        {
            return await _context.Set<FormModule>()
                .Where(fm => fm.Status == true)
                .Include(fm => fm.Form)
                .Include(fm => fm.Module)
                .ToListAsync();
        }

        public async Task<List<FormModule>> GetFormModulesByFormIdAsync(int formId)
        {
            return await _context.Set<FormModule>()
                .Where(fm => fm.Status == true && fm.FormId == formId)
                .Include(fm => fm.Form)
                .Include(fm => fm.Module)
                .ToListAsync();
        }

        public async Task<List<FormModule>> GetFormModulesByModuleIdAsync(int moduleId)
        {
            return await _context.Set<FormModule>()
                .Where(fm => fm.Status == true && fm.ModuleId == moduleId)
                .Include(fm => fm.Form)
                .Include(fm => fm.Module)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(FormModule formModule)
        {
            var existingFormModule = await _context.Set<FormModule>().FindAsync(formModule.Id);
            if (existingFormModule == null) return false;

            // Actualizar sólo si los IDs no son cero
            if (formModule.FormId != 0)
                existingFormModule.FormId = formModule.FormId;

            if (formModule.ModuleId != 0)
                existingFormModule.ModuleId = formModule.ModuleId;

            _context.Set<FormModule>().Update(existingFormModule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
