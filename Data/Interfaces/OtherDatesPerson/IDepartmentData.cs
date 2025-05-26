using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDepartmentData : IBaseModelData<Department>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Department department);
        Task<List<Department>> GetDepartmentsByCountryAsync(int countryId);
        Task<List<Department>> GetActiveAsync();
    }
}
