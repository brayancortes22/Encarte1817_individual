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
    /// Controlador para la gestión de relaciones formulario-módulo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormModuleController : GenericController<FormModule, FormModuleDto>
    {
        private readonly IFormModuleBusiness _formModuleBusiness;

        /// <summary>
        /// Constructor del controlador de relaciones formulario-módulo
        /// </summary>
        /// <param name="formModuleBusiness">Servicio de negocio para relaciones formulario-módulo</param>
        /// <param name="logger">Servicio de logging</param>
        public FormModuleController(
            IFormModuleBusiness formModuleBusiness,
            ILogger<FormModuleController> logger)
            : base(formModuleBusiness, logger)
        {
            _formModuleBusiness = formModuleBusiness;
        }

        // Métodos específicos para FormModule si son necesarios
    }
}
