using Entity.Dtos.OtherDatesPerson.Department;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de departamentos.
    /// </summary>
    public interface IDepartmentBusiness : IBaseBusiness<Department, DepartmentDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un departamento.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del departamento</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialDepartmentAsync(UpdateDepartmentDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del departamento, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del departamento a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicDepartmentAsync(DeleteLogicalDepartmentDto dto);
        
        /// <summary>
        /// Obtiene todos los departamentos activos.
        /// </summary>
        /// <returns>Lista de DTOs de departamentos activos</returns>
        Task<List<DepartmentDto>> GetActiveDepartmentsAsync();
        
        /// <summary>
        /// Obtiene todos los departamentos de un país específico.
        /// </summary>
        /// <param name="countryId">ID del país</param>
        /// <returns>Lista de DTOs de departamentos del país especificado</returns>
        Task<List<DepartmentDto>> GetDepartmentsByCountryAsync(int countryId);
    }
}
