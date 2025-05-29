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
    /// Controlador para la gestión de relaciones rol-usuario
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolUserController : GenericController<RolUser, RolUserDto>
    {
        private readonly IRoleUserBusiness _rolUserBusiness;

        /// <summary>
        /// Constructor del controlador de relaciones rol-usuario
        /// </summary>
        /// <param name="rolUserBusiness">Servicio de negocio para relaciones rol-usuario</param>
        /// <param name="logger">Servicio de logging</param>
        public RolUserController(
            IRoleUserBusiness rolUserBusiness,
            ILogger<RolUserController> logger)
            : base(rolUserBusiness, logger)
        {
            _rolUserBusiness = rolUserBusiness;
        }

        // Métodos específicos para RolUser si son necesarios
    }
}
