using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.City;
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
    /// Implementa la lógica de negocio para la gestión de ciudades.
    /// </summary>
    public class CityBusiness : BaseBusiness<City, CityDto>, ICityBusiness
    {
        private readonly ICityData _cityData;

        /// <summary>
        /// Constructor de la clase CityBusiness.
        /// </summary>
        public CityBusiness(
            ICityData cityData,
            IMapper mapper,
            ILogger<CityBusiness> logger,
            IGenericIHelpers helpers)
            : base(cityData, mapper, logger, helpers)
        {
            _cityData = cityData;
        }

        /// <summary>
        /// Realiza un borrado lógico de una ciudad.
        /// </summary>
        public async Task<bool> DeleteLogicCityAsync(DeleteLogicalCityDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID de la ciudad es inválido");

            var exists = await _cityData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("ciudad", dto.Id);

            return await _cityData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todas las ciudades activas.
        /// </summary>
        public async Task<List<CityDto>> GetActiveCitiesAsync()
        {
            try
            {
                var entities = await _cityData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todas las ciudades activas");
                return _mapper.Map<List<CityDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las ciudades activas: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene las ciudades de un departamento específico.
        /// </summary>
        public async Task<List<CityDto>> GetCitiesByDepartmentAsync(int departmentId)
        {
            if (departmentId <= 0)
                throw new ArgumentException("ID de departamento inválido");

            try
            {
                var entities = await _cityData.GetCitiesByDepartmentAsync(departmentId);
                _logger.LogInformation($"Obteniendo ciudades del departamento con ID: {departmentId}");
                return _mapper.Map<List<CityDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener ciudades del departamento ID {departmentId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente una ciudad.
        /// </summary>
        public async Task<bool> UpdatePartialCityAsync(UpdateCityDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var city = _mapper.Map<City>(dto);
            return await _cityData.UpdatePartial(city);
        }
    }
}
