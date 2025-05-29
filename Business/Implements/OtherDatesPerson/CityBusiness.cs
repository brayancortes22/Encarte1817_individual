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

    }
}
