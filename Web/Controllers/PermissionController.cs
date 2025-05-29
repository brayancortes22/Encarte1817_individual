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
    /// Controlador para la gestión de permisos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionController : GenericController<Permission, PermissionDto>
    {
        private readonly IPermissionBusiness _permissionBusiness;

        /// <summary>
        /// Constructor del controlador de permisos
        /// </summary>
        /// <param name="permissionBusiness">Servicio de negocio para permisos</param>
        /// <param name="logger">Servicio de logging</param>
        public PermissionController(
            IPermissionBusiness permissionBusiness,
            ILogger<PermissionController> logger)
            : base(permissionBusiness, logger)
        {
            _permissionBusiness = permissionBusiness;
        }

        // Métodos específicos para Permission si son necesarios
    }
}
