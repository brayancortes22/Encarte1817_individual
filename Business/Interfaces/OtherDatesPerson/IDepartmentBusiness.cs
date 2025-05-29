using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con departamentos/estados
    /// </summary>
    public interface IDepartmentBusiness : IBaseBusiness<Department, DepartmentDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de departamentos si los necesitas
    }
}
