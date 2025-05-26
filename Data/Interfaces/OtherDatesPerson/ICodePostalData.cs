using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICodePostalData : IBaseModelData<CodePostal>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(CodePostal codePostal);
        Task<List<CodePostal>> GetCodePostalsByDistrictAsync(int districtId);
        Task<CodePostal> GetByCodeAsync(string code);
        Task<List<CodePostal>> GetActiveAsync();
    }
}
