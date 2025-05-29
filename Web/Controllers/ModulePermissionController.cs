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
    /// Controlador para la gestión de permisos por módulo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModulePermissionController : GenericController<ModulePermission, ModulePermissionDto>
    {
        private readonly IModulePermissionBusiness _modulePermissionBusiness;

        /// <summary>
        /// Constructor del controlador de permisos por módulo
        /// </summary>
        /// <param name="modulePermissionBusiness">Servicio de negocio para permisos por módulo</param>
        /// <param name="logger">Servicio de logging</param>
        public ModulePermissionController(
            IModulePermissionBusiness modulePermissionBusiness,
            ILogger<ModulePermissionController> logger)
            : base(modulePermissionBusiness, logger)
        {
            _modulePermissionBusiness = modulePermissionBusiness;
        }

        // Métodos específicos para ModulePermission si son necesarios
    }
}
