﻿using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implements.RolData
{
    public class RolData : BaseModelData<Rol> , IRolData
    {
        public RolData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool active)
        {
            var user = await _context.Set<Rol>().FindAsync(id);
            if (user == null)
                return false;

            user.Status = active;
            _context.Entry(user).Property(u => u.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }        public async Task<bool> UpdatePartial(Rol rol)
        {
            var existingRol = await _context.Set<Rol>().FindAsync(rol.Id);
            if (existingRol == null) return false;
            
            // Actualizar solo los campos necesarios
            if (rol.Name != null)
                existingRol.Name = rol.Name;
                
            if (rol.Description != null)
                existingRol.Description = rol.Description;
            
            _context.Set<Rol>().Update(existingRol);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
