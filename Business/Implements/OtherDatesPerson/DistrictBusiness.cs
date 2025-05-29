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
    }
}
