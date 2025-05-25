using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.Country;
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
    /// Implementa la lógica de negocio para la gestión de países.
    /// </summary>
    public class CountryBusiness : BaseBusiness<Country, CountryDto>, ICountryBusiness
    {
        private readonly ICountryData _countryData;

        /// <summary>
        /// Constructor de la clase CountryBusiness.
        /// </summary>
        public CountryBusiness(
            ICountryData countryData,
            IMapper mapper,
            ILogger<CountryBusiness> logger,
            IGenericIHelpers helpers)
            : base(countryData, mapper, logger, helpers)
        {
            _countryData = countryData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un país.
        /// </summary>
        public async Task<bool> DeleteLogicCountryAsync(DeleteLogicalCountryDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del país es inválido");

            var exists = await _countryData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("país", dto.Id);

            return await _countryData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los países activos.
        /// </summary>
        public async Task<List<CountryDto>> GetActiveCountriesAsync()
        {
            try
            {
                var entities = await _countryData.GetActiveCountriesAsync();
                _logger.LogInformation("Obteniendo todos los países activos");
                return _mapper.Map<List<CountryDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los países activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un país.
        /// </summary>
        public async Task<bool> UpdatePartialCountryAsync(UpdateCountryDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var country = _mapper.Map<Country>(dto);
            return await _countryData.UpdatePartial(country);
        }
    }
}
