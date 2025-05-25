using Entity.Dtos.OtherDatesPerson.Country;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de países.
    /// </summary>
    public interface ICountryBusiness : IBaseBusiness<Country, CountryDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un país.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del país</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialCountryAsync(UpdateCountryDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del país, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del país a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicCountryAsync(DeleteLogicalCountryDto dto);
        
        /// <summary>
        /// Obtiene todos los países activos.
        /// </summary>
        /// <returns>Lista de DTOs de países activos</returns>
        Task<List<CountryDto>> GetActiveCountriesAsync();
    }
}
