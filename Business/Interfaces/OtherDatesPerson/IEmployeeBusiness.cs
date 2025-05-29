using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con empleados
    /// </summary>
    public interface IEmployeeBusiness : IBaseBusiness<Employee, EmployeeDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de empleados si los necesitas
    }
}
