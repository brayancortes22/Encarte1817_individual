using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.CountryData
{
    public class CountryData : BaseModelData<Country>, ICountryData
    {
        public CountryData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var country = await _context.Set<Country>().FindAsync(id);
            if (country == null)
                return false;

            country.Status = status;
            _context.Entry(country).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Country>> GetActiveCountriesAsync()
        {
            return await _context.Set<Country>()
                .Where(c => c.Status == true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Country country)
        {
            var existingCountry = await _context.Set<Country>().FindAsync(country.Id);
            if (existingCountry == null) return false;
            
            // Actualizar solo los campos que son requeridos
            if (country.CountryName != null)
                existingCountry.CountryName = country.CountryName;
            
            if (country.CountryCode != null)
                existingCountry.CountryCode = country.CountryCode;
                
            if (country.Currency != null)
                existingCountry.Currency = country.Currency;
                
            if (country.PhonePrefix != null)
                existingCountry.PhonePrefix = country.PhonePrefix;
            
            _context.Set<Country>().Update(existingCountry);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
