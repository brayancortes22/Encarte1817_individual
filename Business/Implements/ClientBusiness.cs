using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.Client;
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
    /// Implementa la lógica de negocio para la gestión de clientes.
    /// </summary>
    public class ClientBusiness : BaseBusiness<Client, ClientDto>, IClientBusiness
    {
        private readonly IClientData _clientData;

        /// <summary>
        /// Constructor de la clase ClientBusiness.
        /// </summary>
        public ClientBusiness(
            IClientData clientData,
            IMapper mapper,
            ILogger<ClientBusiness> logger,
            IGenericIHelpers helpers)
            : base(clientData, mapper, logger, helpers)
        {
            _clientData = clientData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un cliente.
        /// </summary>
        public async Task<bool> DeleteLogicClientAsync(DeleteLogicalClientDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del cliente es inválido");

            var exists = await _clientData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("cliente", dto.Id);

            return await _clientData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los clientes activos.
        /// </summary>
        public async Task<List<ClientDto>> GetActiveClientsAsync()
        {
            try
            {
                var entities = await _clientData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los clientes activos");
                return _mapper.Map<List<ClientDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los clientes activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un cliente por su número de documento.
        /// </summary>
        public async Task<ClientDto> GetClientByDocumentNumberAsync(string documentNumber)
        {
            if (string.IsNullOrEmpty(documentNumber))
                throw new ArgumentException("Número de documento inválido");

            try
            {
                var entity = await _clientData.GetByDocumentNumberAsync(documentNumber);
                _logger.LogInformation($"Buscando cliente con número de documento: {documentNumber}");
                return _mapper.Map<ClientDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar cliente con documento {documentNumber}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca clientes por nombre.
        /// </summary>
        public async Task<List<ClientDto>> SearchClientsByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nombre de búsqueda inválido");

            try
            {
                var entities = await _clientData.SearchClientsByNameAsync(name);
                _logger.LogInformation($"Buscando clientes con nombre similar a: {name}");
                return _mapper.Map<List<ClientDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar clientes por nombre {name}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un cliente.
        /// </summary>
        public async Task<bool> UpdatePartialClientAsync(UpdateClientDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var client = _mapper.Map<Client>(dto);
            return await _clientData.UpdatePartial(client);
        }
    }
}
