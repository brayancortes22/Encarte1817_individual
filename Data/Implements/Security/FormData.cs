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
    public class FormData : BaseModelData<Form>, IFormData
    {
        public FormData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            if (form == null)
                return false;

            form.Status = status;
            _context.Entry(form).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Form>> GetActiveAsync()
        {
            return await _context.Set<Form>()
                .Where(f => f.Status == true)
                .ToListAsync();
        }

      

        public async Task<bool> UpdatePartial(Form form)
        {
            var existingForm = await _context.Set<Form>().FindAsync(form.Id);
            if (existingForm == null) return false;
            
            // Actualizar solo los campos necesarios
            if (form.Name != null)
                existingForm.Name = form.Name;
                
            if (form.Description != null)
                existingForm.Description = form.Description;
                
            if (form.Url != null)
                existingForm.Url = form.Url;
                
            // Actualizar valores numéricos si no son cero
            if (form.Order != 0)
                existingForm.Order = form.Order;
                
            
            _context.Set<Form>().Update(existingForm);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
