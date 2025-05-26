using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.PermissionData
{
    public class PermissionData : BaseModelData<Permission>, IPermissionData
    {
        public PermissionData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var permission = await _context.Set<Permission>().FindAsync(id);
            if (permission == null)
                return false;

            permission.Status = status;
            _context.Entry(permission).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Permission>> GetActiveAsync()
        {
            return await _context.Set<Permission>()
                .Where(p => p.Status == true)
                .Include(p => p.Module)
                .ToListAsync();
        }

        public async Task<List<Permission>> GetByTypeAsync(int permissionTypeId)
        {
            return await _context.Set<Permission>()
                .Where(p => p.Status == true && (int)p.Type == permissionTypeId)
                .Include(p => p.Module)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Permission permission)
        {
            var existingPermission = await _context.Set<Permission>().FindAsync(permission.Id);
            if (existingPermission == null) return false;
            
            // Actualizar solo los campos necesarios
            if (permission.Name != null)
                existingPermission.Name = permission.Name;
                
            if (permission.Description != null)
                existingPermission.Description = permission.Description;
                
            if (permission.PermissionCode != null)
                existingPermission.PermissionCode = permission.PermissionCode;
                
            // Si el tipo de permiso es diferente de cero (valor por defecto), actualizarlo
            if ((int)permission.Type != 0)
                existingPermission.Type = permission.Type;
            
            _context.Set<Permission>().Update(existingPermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
