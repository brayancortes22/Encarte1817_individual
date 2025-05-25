using Entity.Dtos.OtherDatesPerson.City;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de ciudades.
    /// </summary>
    public interface ICityBusiness : IBaseBusiness<City, CityDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de una ciudad.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados de la ciudad</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialCityAsync(UpdateCityDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico de la ciudad, marcándola como inactiva.
        /// </summary>
        /// <param name="dto">Objeto con el ID de la ciudad a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicCityAsync(DeleteLogicalCityDto dto);
        
        /// <summary>
        /// Obtiene todas las ciudades activas.
        /// </summary>
        /// <returns>Lista de DTOs de ciudades activas</returns>
        Task<List<CityDto>> GetActiveCitiesAsync();
        
        /// <summary>
        /// Obtiene todas las ciudades de un departamento específico.
        /// </summary>
        /// <param name="departmentId">ID del departamento</param>
        /// <returns>Lista de DTOs de ciudades del departamento especificado</returns>
        Task<List<CityDto>> GetCitiesByDepartmentAsync(int departmentId);
    }
}
