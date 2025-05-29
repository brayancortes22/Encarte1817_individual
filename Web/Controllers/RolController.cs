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
    /// Controlador para la gestión de roles
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : GenericController<Rol, RolDto>
    {
        private readonly IRoleBusiness _rolBusiness;

        /// <summary>
        /// Constructor del controlador de roles
        /// </summary>
        /// <param name="rolBusiness">Servicio de negocio para roles</param>
        /// <param name="logger">Servicio de logging</param>
        public RolController(
            IRoleBusiness rolBusiness,
            ILogger<RolController> logger)
            : base(rolBusiness, logger)
        {
            _rolBusiness = rolBusiness;
        }

        // Métodos específicos para Rol si son necesarios
    }
}
