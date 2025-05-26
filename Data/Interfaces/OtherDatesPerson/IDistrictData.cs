using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDistrictData : IBaseModelData<District>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(District district);
        Task<List<District>> GetDistrictsByCityAsync(int cityId);
        Task<List<District>> GetActiveAsync();
    }
}
