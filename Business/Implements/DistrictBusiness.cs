using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.District;
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
    /// Implementa la lógica de negocio para la gestión de distritos.
    /// </summary>
    public class DistrictBusiness : BaseBusiness<District, DistrictDto>, IDistrictBusiness
    {
        private readonly IDistrictData _districtData;

        /// <summary>
        /// Constructor de la clase DistrictBusiness.
        /// </summary>
        public DistrictBusiness(
            IDistrictData districtData,
            IMapper mapper,
            ILogger<DistrictBusiness> logger,
            IGenericIHelpers helpers)
            : base(districtData, mapper, logger, helpers)
        {
            _districtData = districtData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un distrito.
        /// </summary>
        public async Task<bool> DeleteLogicDistrictAsync(DeleteLogicalDistrictDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del distrito es inválido");

            var exists = await _districtData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("distrito", dto.Id);

            return await _districtData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los distritos activos.
        /// </summary>
        public async Task<List<DistrictDto>> GetActiveDistrictsAsync()
        {
            try
            {
                var entities = await _districtData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los distritos activos");
                return _mapper.Map<List<DistrictDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los distritos activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los distritos de una ciudad específica.
        /// </summary>
        public async Task<List<DistrictDto>> GetDistrictsByCityAsync(int cityId)
        {
            if (cityId <= 0)
                throw new ArgumentException("ID de ciudad inválido");

            try
            {
                var entities = await _districtData.GetDistrictsByCityAsync(cityId);
                _logger.LogInformation($"Obteniendo distritos de la ciudad con ID: {cityId}");
                return _mapper.Map<List<DistrictDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener distritos de la ciudad ID {cityId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un distrito.
        /// </summary>
        public async Task<bool> UpdatePartialDistrictAsync(UpdateDistrictDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var district = _mapper.Map<District>(dto);
            return await _districtData.UpdatePartial(district);
        }
    }
}
