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

    }
}
