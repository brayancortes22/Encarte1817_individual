using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.PersonData
{
    public class PersonData : BaseModelData<Person>, IPersonData
    {
        public PersonData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var person = await _context.Set<Person>().FindAsync(id);
            if (person == null)
                return false;

            person.Status = status;
            _context.Entry(person).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Person> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Set<Person>()
                .Include(p => p.Country)
                .Include(p => p.Department)
                .Include(p => p.City)
                .Include(p => p.District)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.IdentificationNumber == documentNumber);
        }

        public async Task<List<Person>> SearchPersonsByNameAsync(string name)
        {
            return await _context.Set<Person>()
                .Include(p => p.Country)
                .Include(p => p.Department)
                .Include(p => p.City)
                .Include(p => p.District)
                .Where(p => p.Status == true && 
                      (p.Name.Contains(name) || 
                       p.FirstName.Contains(name) ||
                       p.LastName.Contains(name)))
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Person person)
        {
            var existingPerson = await _context.Set<Person>().FindAsync(person.Id);
            if (existingPerson == null) return false;
            
            // Actualizar solo los campos necesarios
            if (person.Name != null)
                existingPerson.Name = person.Name;
                
            if (person.FirstName != null)
                existingPerson.FirstName = person.FirstName;
                
            if (person.LastName != null)
                existingPerson.LastName = person.LastName;
                
            if (person.Email != null)
                existingPerson.Email = person.Email;
                
            if (person.PhoneNumber != null)
                existingPerson.PhoneNumber = person.PhoneNumber;
                
            if (person.Address != null)
                existingPerson.Address = person.Address;
                
            if (person.Gender != null)
                existingPerson.Gender = person.Gender;
                
            if (person.TypeIdentification != null)
                existingPerson.TypeIdentification = person.TypeIdentification;
                
            if (person.IdentificationNumber != null)
                existingPerson.IdentificationNumber = person.IdentificationNumber;
                
            if (person.PostalCode != null)
                existingPerson.PostalCode = person.PostalCode;
                
            if (person.ProfilePictureUrl != null)
                existingPerson.ProfilePictureUrl = person.ProfilePictureUrl;
                
            // Si las fechas no son predeterminadas, actualizarlas
            if (person.BirthDate.HasValue && person.BirthDate != default(DateTime))
                existingPerson.BirthDate = person.BirthDate;
                
            // Si los IDs son diferentes de cero o nulos, actualizarlos
            if (person.CountryId.HasValue && person.CountryId.Value != 0)
                existingPerson.CountryId = person.CountryId;
                
            if (person.DepartmentId.HasValue && person.DepartmentId.Value != 0)
                existingPerson.DepartmentId = person.DepartmentId;
                
            if (person.CityId.HasValue && person.CityId.Value != 0)
                existingPerson.CityId = person.CityId;
                
            if (person.DistrictId.HasValue && person.DistrictId.Value != 0)
                existingPerson.DistrictId = person.DistrictId;
            
            _context.Set<Person>().Update(existingPerson);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
