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
    public class RolFormPermissionData : BaseModelData<RolFormPermission>, IRolFormPermissionData
    {
        public RolFormPermissionData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var rolFormPermission = await _context.Set<RolFormPermission>().FindAsync(id);
            if (rolFormPermission == null)
                return false;

            rolFormPermission.Status = status;
            _context.Entry(rolFormPermission).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RolFormPermission>> GetActiveAsync()
        {
            return await _context.Set<RolFormPermission>()
                .Where(rfp => rfp.Status == true)
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .ToListAsync();
        }

        public async Task<List<RolFormPermission>> GetByFormIdAsync(int formId)
        {
            return await _context.Set<RolFormPermission>()
                .Where(rfp => rfp.Status == true && rfp.FormId == formId)
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .ToListAsync();
        }

        public async Task<List<RolFormPermission>> GetByPermissionIdAsync(int permissionId)
        {
            return await _context.Set<RolFormPermission>()
                .Where(rfp => rfp.Status == true && rfp.PermissionId == permissionId)
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .ToListAsync();
        }

        public async Task<RolFormPermission> GetByRolFormPermissionAsync(int rolId, int formId, int permissionId)
        {
            return await _context.Set<RolFormPermission>()
                .Where(rfp => rfp.Status == true && 
                       rfp.RolId == rolId && 
                       rfp.FormId == formId && 
                       rfp.PermissionId == permissionId)
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .FirstOrDefaultAsync();
        }

        public async Task<List<RolFormPermission>> GetByRolIdAsync(int rolId)
        {
            return await _context.Set<RolFormPermission>()
                .Where(rfp => rfp.Status == true && rfp.RolId == rolId)
                .Include(rfp => rfp.Rol)
                .Include(rfp => rfp.Form)
                .Include(rfp => rfp.Permission)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(RolFormPermission rolFormPermission)
        {
            var existingRolFormPermission = await _context.Set<RolFormPermission>().FindAsync(rolFormPermission.Id);
            if (existingRolFormPermission == null) return false;
            
            // Actualizar solo si los IDs no son cero
            if (rolFormPermission.RolId != 0)
                existingRolFormPermission.RolId = rolFormPermission.RolId;
                
            if (rolFormPermission.FormId != 0)
                existingRolFormPermission.FormId = rolFormPermission.FormId;
                
            if (rolFormPermission.PermissionId != 0)
                existingRolFormPermission.PermissionId = rolFormPermission.PermissionId;
            
            _context.Set<RolFormPermission>().Update(existingRolFormPermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
