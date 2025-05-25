using Entity.Dtos.OtherDatesPerson.District;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de distritos.
    /// </summary>
    public interface IDistrictBusiness : IBaseBusiness<District, DistrictDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un distrito.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del distrito</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialDistrictAsync(UpdateDistrictDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del distrito, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del distrito a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicDistrictAsync(DeleteLogicalDistrictDto dto);
        
        /// <summary>
        /// Obtiene todos los distritos activos.
        /// </summary>
        /// <returns>Lista de DTOs de distritos activos</returns>
        Task<List<DistrictDto>> GetActiveDistrictsAsync();
        
        /// <summary>
        /// Obtiene todos los distritos de una ciudad específica.
        /// </summary>
        /// <param name="cityId">ID de la ciudad</param>
        /// <returns>Lista de DTOs de distritos de la ciudad especificada</returns>
        Task<List<DistrictDto>> GetDistrictsByCityAsync(int cityId);
    }
}
