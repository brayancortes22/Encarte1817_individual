using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.EmployeeData
{
    public class EmployeeData : BaseModelData<Employee>, IEmployeeData
    {
        public EmployeeData(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> ActiveAsync(int id, bool status)
        {
            var employee = await _context.Set<Employee>().FindAsync(id);
            if (employee == null)
                return false;

            employee.Status = status;
            _context.Entry(employee).Property(x => x.Status).IsModified = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Set<Employee>()
                .Include(e => e.Person)                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Person.IdentificationNumber == documentNumber && e.Status == true);
        }

        public async Task<List<Employee>> GetActiveAsync()
        {
            return await _context.Set<Employee>()
                .Where(e => e.Status == true)
                .Include(e => e.Person)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesByContractTypeAsync(int contractTypeId)
        {
            return await _context.Set<Employee>()
                .Where(e => e.Status == true && (int)e.ContractType == contractTypeId)
                .Include(e => e.Person)
                .Include(e => e.User)
                .ToListAsync();
        }

        public async Task<List<Employee>> SearchEmployeesByNameAsync(string name)
        {
            return await _context.Set<Employee>()
                .Include(e => e.Person)
                .Include(e => e.User)                .Where(e => e.Status == true && 
                      (e.Person.Name.Contains(name) || 
                       e.Person.FirstName.Contains(name) || 
                       e.Person.LastName.Contains(name)))
                .ToListAsync();
        }

        public async Task<bool> UpdatePartial(Employee employee)
        {
            var existingEmployee = await _context.Set<Employee>().FindAsync(employee.Id);
            if (existingEmployee == null) return false;
            
            // Actualizar solo los campos necesarios
            if (employee.Position != null)
                existingEmployee.Position = employee.Position;
                
            if (employee.Department != null)
                existingEmployee.Department = employee.Department;
                
            if (employee.EmployeeCode != null)
                existingEmployee.EmployeeCode = employee.EmployeeCode;
                
            if (employee.WorkEmail != null)
                existingEmployee.WorkEmail = employee.WorkEmail;
            
            // Si los valores numéricos son diferentes de cero, actualizarlos
            if (employee.Salary != 0)
                existingEmployee.Salary = employee.Salary;
                
            // Si las fechas no son predeterminadas, actualizarlas
            if (employee.HiringDate != default(DateTime))
                existingEmployee.HiringDate = employee.HiringDate;

            // Si el tipo de contrato es diferente de cero (valor por defecto), actualizarlo
            if ((int)employee.ContractType != 0)
                existingEmployee.ContractType = employee.ContractType;
            
            _context.Set<Employee>().Update(existingEmployee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
