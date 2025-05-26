using Entity.Dtos.Security.Person;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Define los métodos de negocio específicos para la gestión de personas.
    /// </summary>
    public interface IPersonBusiness : IBaseBusiness<Person, PersonDto>
    {
        /// <summary>
        /// Actualiza parcialmente los datos de una persona.
        /// </summary>
        /// <param name="dto">Objeto con los datos actualizados de la persona</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario false</returns>
        Task<bool> UpdatePartialPersonAsync(UpdatePersonDto dto);
        
        /// <summary>
        /// Realiza un borrado lógico de la persona, marcándola como inactiva.
        /// </summary>
        /// <param name="dto">Objeto con el ID de la persona a desactivar y su nuevo estado</param>
        /// <returns>True si el borrado lógico fue exitoso; de lo contrario false</returns>
        Task<bool> DeleteLogicPersonAsync(DeleteLogicalPersonDto dto);
        
        /// <summary>
        /// Busca una persona por su número de documento de identidad.
        /// </summary>
        /// <param name="documentNumber">Número de documento</param>
        /// <returns>DTO de la persona encontrada o null si no existe</returns>
        Task<PersonDto> GetPersonByDocumentNumberAsync(string documentNumber);
        
        /// <summary>
        /// Busca personas que coincidan con el nombre especificado.
        /// </summary>
        /// <param name="name">Nombre o parte del nombre a buscar</param>
        /// <returns>Lista de DTOs de personas que coinciden con la búsqueda</returns>
        Task<List<PersonDto>> SearchPersonsByNameAsync(string name);
    }
}
