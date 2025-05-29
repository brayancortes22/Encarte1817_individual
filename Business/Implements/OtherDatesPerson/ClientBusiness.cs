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
    /// Implementa la lógica de negocio para la gestión de clientes.
    /// </summary>
    public class ClientBusiness : BaseBusiness<Client, ClientDto>, IClientBusiness
    {
        private readonly IClientData _clientData;

        /// <summary>
        /// Constructor de la clase ClientBusiness.
        /// </summary>
        public ClientBusiness(
            IClientData data,
            IMapper mapper,
            ILogger<ClientBusiness> logger,
            IGenericIHelpers helpers)
            : base(data, mapper, logger, helpers)
        {
            _clientData = data;
        }

        
    }
}
