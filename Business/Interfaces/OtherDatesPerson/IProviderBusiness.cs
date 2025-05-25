using Entity.Dtos.OtherDatesPerson.Provider;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de proveedores.
    /// </summary>
    public interface IProviderBusiness : IBaseBusiness<Provider, ProviderDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un proveedor.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del proveedor</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialProviderAsync(UpdateProviderDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del proveedor, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del proveedor a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicProviderAsync(DeleteLogicalProviderDto dto);
        
        /// <summary>
        /// Obtiene todos los proveedores activos.
        /// </summary>
        /// <returns>Lista de DTOs de proveedores activos</returns>
        Task<List<ProviderDto>> GetActiveProvidersAsync();
        
        /// <summary>
        /// Busca un proveedor por su número de documento o identificación tributaria.
        /// </summary>
        /// <param name="documentNumber">Número de documento o NIT</param>
        /// <returns>DTO del proveedor encontrado o null si no existe</returns>
        Task<ProviderDto> GetProviderByDocumentNumberAsync(string documentNumber);
        
        /// <summary>
        /// Busca proveedores que coincidan con el nombre especificado.
        /// </summary>
        /// <param name="name">Nombre o parte del nombre a buscar</param>
        /// <returns>Lista de DTOs de proveedores que coinciden con la búsqueda</returns>
        Task<List<ProviderDto>> SearchProvidersByNameAsync(string name);
    }
}
