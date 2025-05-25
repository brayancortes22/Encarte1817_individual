using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IEmployeeData : IBaseModelData<Employee>
    {
        Task<bool> ActiveAsync(int id, bool status);
        Task<bool> UpdatePartial(Employee employee);
        Task<Employee> GetByDocumentNumberAsync(string documentNumber);
        Task<List<Employee>> GetActiveAsync();
        Task<List<Employee>> SearchEmployeesByNameAsync(string name);
        Task<List<Employee>> GetEmployeesByContractTypeAsync(int contractTypeId);
    }
}
