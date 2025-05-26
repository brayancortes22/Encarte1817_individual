using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICityData : IBaseModelData<City>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(City city);
        Task<List<City>> GetCitiesByDepartmentAsync(int departmentId);
        Task<List<City>> GetActiveAsync();
    }
}
