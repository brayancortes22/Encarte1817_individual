using Entity.Dtos.OtherDatesPerson.Employee;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de empleados.
    /// </summary>
    public interface IEmployeeBusiness : IBaseBusiness<Employee, EmployeeDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un empleado.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del empleado</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialEmployeeAsync(UpdateEmployeeDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del empleado, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del empleado a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicEmployeeAsync(DeleteLogicalEmployeeDto dto);
        
        /// <summary>
        /// Obtiene todos los empleados activos.
        /// </summary>
        /// <returns>Lista de DTOs de empleados activos</returns>
        Task<List<EmployeeDto>> GetActiveEmployeesAsync();
        
        /// <summary>
        /// Busca un empleado por su número de documento de identidad.
        /// </summary>
        /// <param name="documentNumber">Número de documento</param>
        /// <returns>DTO del empleado encontrado o null si no existe</returns>
        Task<EmployeeDto> GetEmployeeByDocumentNumberAsync(string documentNumber);
        
        /// <summary>
        /// Busca empleados que coincidan con el nombre especificado.
        /// </summary>
        /// <param name="name">Nombre o parte del nombre a buscar</param>
        /// <returns>Lista de DTOs de empleados que coinciden con la búsqueda</returns>
        Task<List<EmployeeDto>> SearchEmployeesByNameAsync(string name);
        
        /// <summary>
        /// Obtiene los empleados por tipo de contrato.
        /// </summary>
        /// <param name="contractTypeId">ID del tipo de contrato</param>
        /// <returns>Lista de DTOs de empleados con el tipo de contrato especificado</returns>
        Task<List<EmployeeDto>> GetEmployeesByContractTypeAsync(int contractTypeId);
    }
}
