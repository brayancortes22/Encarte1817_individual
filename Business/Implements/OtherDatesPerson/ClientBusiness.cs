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
            IClientData clientData,
            IMapper mapper,
            ILogger<ClientBusiness> logger,
            IGenericIHelpers helpers)
            : base(clientData, mapper, logger, helpers)
        {
            _clientData = clientData;
        }

        
    }
}
