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
    /// Controlador para la gestión de módulos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ModuleController : GenericController<Module, ModuleDto>
    {
        private readonly IModuleBusiness _moduleBusiness;

        /// <summary>
        /// Constructor del controlador de módulos
        /// </summary>
        /// <param name="moduleBusiness">Servicio de negocio para módulos</param>
        /// <param name="logger">Servicio de logging</param>
        public ModuleController(
            IModuleBusiness moduleBusiness,
            ILogger<ModuleController> logger)
            : base(moduleBusiness, logger)
        {
            _moduleBusiness = moduleBusiness;
        }

        // Métodos específicos para Module si son necesarios
    }
}
