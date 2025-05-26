using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.CodePostalData
{
    public class CodePostalData : BaseModelData<CodePostal>, ICodePostalData
    {
        public CodePostalData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var codePostal = await _context.Set<CodePostal>().FindAsync(id);
            if (codePostal == null)
                return false;

            codePostal.Status = status;
            _context.Entry(codePostal).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CodePostal> GetByCodeAsync(string code)
        {
            return await _context.Set<CodePostal>()
                .FirstOrDefaultAsync(c => c.postalCode == code && c.Status == true);
        }

        public async Task<List<CodePostal>> GetActiveAsync()
        {
            return await _context.Set<CodePostal>()
                .Where(c => c.Status == true)
                .ToListAsync();
        }

        public async Task<List<CodePostal>> GetCodePostalsByDistrictAsync(int districtId)
        {
            return await _context.Set<CodePostal>()
                .Where(c => c.Status == true)
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(CodePostal codePostal)
        {
            var existingCodePostal = await _context.Set<CodePostal>().FindAsync(codePostal.Id);
            if (existingCodePostal == null) return false;
            
            // Actualizar solo los campos necesarios
            if (codePostal.postalCode != null)
                existingCodePostal.postalCode = codePostal.postalCode;
                
            if (codePostal.area != null)
                existingCodePostal.area = codePostal.area;
            
            _context.Set<CodePostal>().Update(existingCodePostal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
