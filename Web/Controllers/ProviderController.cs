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
    /// Controlador para la gestión de proveedores
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProviderController : GenericController<Provider, ProviderDto>
    {
        private readonly IProviderBusiness _providerBusiness;

        /// <summary>
        /// Constructor del controlador de proveedores
        /// </summary>
        /// <param name="providerBusiness">Servicio de negocio para proveedores</param>
        /// <param name="logger">Servicio de logging</param>
        public ProviderController(
            IProviderBusiness providerBusiness,
            ILogger<ProviderController> logger)
            : base(providerBusiness, logger)
        {
            _providerBusiness = providerBusiness;
        }

        // Puedes agregar métodos específicos para la entidad Provider aquí si es necesario
    }
}
