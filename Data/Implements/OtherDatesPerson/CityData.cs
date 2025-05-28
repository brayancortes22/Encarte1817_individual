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
    public class CityData : BaseModelData<City>, ICityData
    {
        public CityData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var city = await _context.Set<City>().FindAsync(id);
            if (city == null)
                return false;

            city.Status = status;
            _context.Entry(city).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<City>> GetActiveAsync()
        {
            return await _context.Set<City>()
                .Where(c => c.Status == true)
                .ToListAsync();
        }

        public async Task<List<City>> GetCitiesByDepartmentAsync(int departmentId)
        {
            return await _context.Set<City>()
                .Where(c => c.Department.Id == departmentId && c.Status == true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(City city)
        {
            var existingCity = await _context.Set<City>().FindAsync(city.Id);
            if (existingCity == null) return false;
            
            // Actualizar solo los campos necesarios
            if (city.CityName != null)
                existingCity.CityName = city.CityName;
                
            if (city.CityCode != null)
                existingCity.CityCode = city.CityCode;
            
            _context.Set<City>().Update(existingCity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
