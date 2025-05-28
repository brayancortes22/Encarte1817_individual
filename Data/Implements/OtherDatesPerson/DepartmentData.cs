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
    public class DepartmentData : BaseModelData<Department>, IDepartmentData
    {
        public DepartmentData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var department = await _context.Set<Department>().FindAsync(id);
            if (department == null)
                return false;

            department.Status = status;
            _context.Entry(department).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Department>> GetActiveAsync()
        {
            return await _context.Set<Department>()
                .Where(d => d.Status == true)
                .ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsByCountryAsync(int countryId)
        {
            return await _context.Set<Department>()
                .Where(d => d.Country.Id == countryId && d.Status == true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Department department)
        {
            var existingDepartment = await _context.Set<Department>().FindAsync(department.Id);
            if (existingDepartment == null) return false;
            
            // Actualizar solo los campos necesarios
            if (department.DepartmentName != null)
                existingDepartment.DepartmentName = department.DepartmentName;
                
            if (department.DepartmentCode != null)
                existingDepartment.DepartmentCode = department.DepartmentCode;
            
            _context.Set<Department>().Update(existingDepartment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
