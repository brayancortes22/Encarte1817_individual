using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos;
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

        
    }
}
