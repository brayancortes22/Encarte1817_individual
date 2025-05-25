using Entity.Dtos.OtherDatesPerson.Client;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de clientes.
    /// </summary>
    public interface IClientBusiness : IBaseBusiness<Client, ClientDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de un cliente.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados del cliente</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialClientAsync(UpdateClientDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico del cliente, marcándolo como inactivo.
        /// </summary>
        /// <param name="dto">Objeto con el ID del cliente a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicClientAsync(DeleteLogicalClientDto dto);
        
        /// <summary>
        /// Obtiene todos los clientes activos.
        /// </summary>
        /// <returns>Lista de DTOs de clientes activos</returns>
        Task<List<ClientDto>> GetActiveClientsAsync();
        
        /// <summary>
        /// Busca un cliente por su número de documento de identidad.
        /// </summary>
        /// <param name="documentNumber">Número de documento</param>
        /// <returns>DTO del cliente encontrado o null si no existe</returns>
        Task<ClientDto> GetClientByDocumentNumberAsync(string documentNumber);
        
        /// <summary>
        /// Busca clientes que coincidan con el nombre especificado.
        /// </summary>
        /// <param name="name">Nombre o parte del nombre a buscar</param>
        /// <returns>Lista de DTOs de clientes que coinciden con la búsqueda</returns>
        Task<List<ClientDto>> SearchClientsByNameAsync(string name);
    }
}
