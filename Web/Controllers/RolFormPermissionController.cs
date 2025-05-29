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
    /// Controlador para la gestión de permisos de formularios por rol
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolFormPermissionController : GenericController<RolFormPermission, RolFormPermissionDto>
    {
        private readonly IRolFormPermissionBusiness _rolFormPermissionBusiness;

        /// <summary>
        /// Constructor del controlador de permisos de formularios por rol
        /// </summary>
        /// <param name="rolFormPermissionBusiness">Servicio de negocio para permisos de formularios por rol</param>
        /// <param name="logger">Servicio de logging</param>
        public RolFormPermissionController(
            IRolFormPermissionBusiness rolFormPermissionBusiness,
            ILogger<RolFormPermissionController> logger)
            : base(rolFormPermissionBusiness, logger)
        {
            _rolFormPermissionBusiness = rolFormPermissionBusiness;
        }

        // Métodos específicos para RolFormPermission si son necesarios
    }
}
