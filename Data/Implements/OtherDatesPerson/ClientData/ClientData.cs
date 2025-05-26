using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.ClientData
{
    public class ClientData : BaseModelData<Client>, IClientData
    {
        public ClientData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var client = await _context.Set<Client>().FindAsync(id);
            if (client == null)
                return false;

            client.Status = status;
            _context.Entry(client).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }        public async Task<Client> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Set<Client>()
                .Include(c => c.Person)
                .FirstOrDefaultAsync(c => c.Person.IdentificationNumber == documentNumber && c.Status == true);
        }

        public async Task<List<Client>> GetActiveAsync()
        {
            return await _context.Set<Client>()
                .Where(c => c.Status == true)
                .Include(c => c.Person)
                .ToListAsync();
        }

        public async Task<List<Client>> SearchClientsByNameAsync(string name)
        {
            return await _context.Set<Client>()
                .Include(c => c.Person)                .Where(c => c.Status == true && 
                      (c.Person.Name.Contains(name) || 
                       c.Person.FirstName.Contains(name) || 
                       c.Person.LastName.Contains(name)))
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Client client)
        {
            var existingClient = await _context.Set<Client>().FindAsync(client.Id);
            if (existingClient == null) return false;
            
            // Actualizar solo los campos necesarios
            if (client.ClientCode != null)
                existingClient.ClientCode = client.ClientCode;
                
            if (client.ClientType != null)
                existingClient.ClientType = client.ClientType;
            
            // Si la fecha de registro no es predeterminada, actualizarla
            if (client.RegistrationDate != default(DateTime))
                existingClient.RegistrationDate = client.RegistrationDate;
            
            _context.Set<Client>().Update(existingClient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
