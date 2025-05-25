using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.ProviderData
{
    public class ProviderData : BaseModelData<Provider>, IProviderData
    {
        public ProviderData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var provider = await _context.Set<Provider>().FindAsync(id);
            if (provider == null)
                return false;

            provider.Status = status;
            _context.Entry(provider).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Provider> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Set<Provider>()                .Include(p => p.Person)
                .FirstOrDefaultAsync(p => p.Person.IdentificationNumber == documentNumber || p.TaxId == documentNumber);
        }

        public async Task<List<Provider>> GetActiveAsync()
        {
            return await _context.Set<Provider>()
                .Where(p => p.Status == true)
                .Include(p => p.Person)
                .ToListAsync();
        }

        public async Task<List<Provider>> SearchProvidersByNameAsync(string name)
        {
            return await _context.Set<Provider>()
                .Include(p => p.Person)
                .Where(p => p.Status == true &&                      (p.CompanyName.Contains(name) || 
                       p.ContactPerson.Contains(name) ||
                       p.Person.Name.Contains(name) || 
                       p.Person.FirstName.Contains(name) || 
                       p.Person.LastName.Contains(name)))
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Provider provider)
        {
            var existingProvider = await _context.Set<Provider>().FindAsync(provider.Id);
            if (existingProvider == null) return false;
            
            // Actualizar solo los campos necesarios
            if (provider.CompanyName != null)
                existingProvider.CompanyName = provider.CompanyName;
                
            if (provider.TaxId != null)
                existingProvider.TaxId = provider.TaxId;
                
            if (provider.Address != null)
                existingProvider.Address = provider.Address;
                
            if (provider.ContactPerson != null)
                existingProvider.ContactPerson = provider.ContactPerson;
                
            if (provider.ServiceType != null)
                existingProvider.ServiceType = provider.ServiceType;
            
            _context.Set<Provider>().Update(existingProvider);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
