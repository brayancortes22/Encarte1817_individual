using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business.Interfaces;
using Entity.Dtos;
using Entity.Model;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la gestión de clientes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : GenericController<Client, ClientDto>
    {
        private readonly IClientBusiness _clientBusiness;

        /// <summary>
        /// Constructor del controlador de clientes
        /// </summary>
        /// <param name="clientBusiness">Servicio de negocio para clientes</param>
        /// <param name="logger">Servicio de logging</param>
        public ClientController(
            IClientBusiness clientBusiness,
            ILogger<ClientController> logger)
            : base(clientBusiness, logger)
        {
            _clientBusiness = clientBusiness;
        }

        // Puedes agregar métodos específicos para la entidad Client aquí si es necesario
    }
}
