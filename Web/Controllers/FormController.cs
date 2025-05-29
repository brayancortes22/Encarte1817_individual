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
    /// Controlador para la gestión de formularios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormController : GenericController<Form, FormDto>
    {
        private readonly IFormBusiness _formBusiness;

        /// <summary>
        /// Constructor del controlador de formularios
        /// </summary>
        /// <param name="formBusiness">Servicio de negocio para formularios</param>
        /// <param name="logger">Servicio de logging</param>
        public FormController(
            IFormBusiness formBusiness,
            ILogger<FormController> logger)
            : base(formBusiness, logger)
        {
            _formBusiness = formBusiness;
        }

        // Métodos específicos para Form si son necesarios
    }
}
