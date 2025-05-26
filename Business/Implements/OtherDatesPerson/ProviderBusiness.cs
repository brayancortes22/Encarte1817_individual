using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.Provider;
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
    /// Implementa la lógica de negocio para la gestión de proveedores.
    /// </summary>
    public class ProviderBusiness : BaseBusiness<Provider, ProviderDto>, IProviderBusiness
    {
        private readonly IProviderData _providerData;

        /// <summary>
        /// Constructor de la clase ProviderBusiness.
        /// </summary>
        public ProviderBusiness(
            IProviderData providerData,
            IMapper mapper,
            ILogger<ProviderBusiness> logger,
            IGenericIHelpers helpers)
            : base(providerData, mapper, logger, helpers)
        {
            _providerData = providerData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un proveedor.
        /// </summary>
        public async Task<bool> DeleteLogicProviderAsync(DeleteLogicalProviderDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del proveedor es inválido");

            var exists = await _providerData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("proveedor", dto.Id);

            return await _providerData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los proveedores activos.
        /// </summary>
        public async Task<List<ProviderDto>> GetActiveProvidersAsync()
        {
            try
            {
                var entities = await _providerData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los proveedores activos");
                return _mapper.Map<List<ProviderDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los proveedores activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un proveedor por su número de documento o identificación tributaria.
        /// </summary>
        public async Task<ProviderDto> GetProviderByDocumentNumberAsync(string documentNumber)
        {
            if (string.IsNullOrEmpty(documentNumber))
                throw new ArgumentException("Número de documento/NIT inválido");

            try
            {
                var entity = await _providerData.GetByDocumentNumberAsync(documentNumber);
                _logger.LogInformation($"Buscando proveedor con número de documento/NIT: {documentNumber}");
                return _mapper.Map<ProviderDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar proveedor con documento/NIT {documentNumber}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca proveedores por nombre.
        /// </summary>
        public async Task<List<ProviderDto>> SearchProvidersByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nombre inválido para la búsqueda");

            try
            {
                var entities = await _providerData.SearchProvidersByNameAsync(name);
                _logger.LogInformation($"Buscando proveedores con el nombre: {name}");
                return _mapper.Map<List<ProviderDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar proveedores con el nombre {name}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un proveedor.
        /// </summary>
        public async Task<bool> UpdatePartialProviderAsync(UpdateProviderDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var provider = _mapper.Map<Provider>(dto);
            return await _providerData.UpdatePartial(provider);
        }
    }
}
