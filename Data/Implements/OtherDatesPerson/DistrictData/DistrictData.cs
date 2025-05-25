using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.DistrictData
{
    public class DistrictData : BaseModelData<District>, IDistrictData
    {
        public DistrictData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var district = await _context.Set<District>().FindAsync(id);
            if (district == null)
                return false;

            district.Status = status;
            _context.Entry(district).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<District>> GetActiveAsync()
        {
            return await _context.Set<District>()
                .Where(d => d.Status == true)
                .ToListAsync();
        }

        public async Task<List<District>> GetDistrictsByCityAsync(int cityId)
        {
            return await _context.Set<District>()
                .Where(d => d.Status == true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(District district)
        {
            var existingDistrict = await _context.Set<District>().FindAsync(district.Id);
            if (existingDistrict == null) return false;
            
            // Actualizar solo los campos necesarios
            if (district.DistrictName != null)
                existingDistrict.DistrictName = district.DistrictName;
                
            if (district.StreetNumber != null)
                existingDistrict.StreetNumber = district.StreetNumber;
                
            if (district.SecondaryNumber != null)
                existingDistrict.SecondaryNumber = district.SecondaryNumber;
                
            if (district.TertiaryNumber != null)
                existingDistrict.TertiaryNumber = district.TertiaryNumber;
                
            if (district.AdditionalNumber != null)
                existingDistrict.AdditionalNumber = district.AdditionalNumber;
            
            // Actualizamos las enumeraciones solo si son diferentes de cero (valor por defecto)
            if ((int)district.StreetType != 0)
                existingDistrict.StreetType = district.StreetType;
                
            if ((int)district.StreetLetter != 0)
                existingDistrict.StreetLetter = district.StreetLetter;
                
            if ((int)district.SecondaryLetter != 0)
                existingDistrict.SecondaryLetter = district.SecondaryLetter;
                
            if ((int)district.AdditionalLetter != 0)
                existingDistrict.AdditionalLetter = district.AdditionalLetter;
            
            _context.Set<District>().Update(existingDistrict);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
