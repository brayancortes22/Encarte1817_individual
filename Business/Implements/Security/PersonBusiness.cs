using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.Person;
using Entity.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Exceptions;
using Utilities.Interfaces;

namespace Business.Implements
{
    /// <summary>
    /// Implementa la lógica de negocio para la gestión de personas.
    /// </summary>
    public class PersonBusiness : BaseBusiness<Person, PersonDto>, IPersonBusiness
    {
        private readonly IPersonData _personData;

        /// <summary>
        /// Constructor de la clase PersonBusiness.
        /// </summary>
        public PersonBusiness(
            IPersonData personData,
            IMapper mapper,
            ILogger<PersonBusiness> logger,
            IGenericIHelpers helpers)
            : base(personData, mapper, logger, helpers)
        {
            _personData = personData;
        }

        /// <summary>
        /// Realiza un borrado lógico de una persona.
        /// </summary>
        public async Task<bool> DeleteLogicPersonAsync(DeleteLogicalPersonDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID de la persona es inválido");

            var exists = await _personData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("persona", dto.Id);

            return await _personData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene una persona por su número de documento.
        /// </summary>
        public async Task<PersonDto> GetPersonByDocumentNumberAsync(string documentNumber)
        {
            if (string.IsNullOrEmpty(documentNumber))
                throw new ArgumentException("Número de documento inválido");

            try
            {
                var entity = await _personData.GetByDocumentNumberAsync(documentNumber);
                _logger.LogInformation($"Buscando persona con número de documento: {documentNumber}");
                return _mapper.Map<PersonDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar persona con documento {documentNumber}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca personas por nombre.
        /// </summary>
        public async Task<List<PersonDto>> SearchPersonsByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nombre de búsqueda inválido");

            try
            {
                var entities = await _personData.SearchPersonsByNameAsync(name);
                _logger.LogInformation($"Buscando personas con nombre similar a: {name}");
                return _mapper.Map<List<PersonDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar personas por nombre {name}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente una persona.
        /// </summary>
        public async Task<bool> UpdatePartialPersonAsync(UpdatePersonDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var person = _mapper.Map<Person>(dto);
            return await _personData.UpdatePartial(person);
        }
    }
}
