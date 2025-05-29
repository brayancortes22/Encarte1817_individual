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
    /// Controlador para la gestión de personas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : GenericController<Person, PersonDto>
    {
        private readonly IPersonBusiness _personBusiness;

        /// <summary>
        /// Constructor del controlador de personas
        /// </summary>
        /// <param name="personBusiness">Servicio de negocio para personas</param>
        /// <param name="logger">Servicio de logging</param>
        public PersonController(
            IPersonBusiness personBusiness,
            ILogger<PersonController> logger)
            : base(personBusiness, logger)
        {
            _personBusiness = personBusiness;
        }

        // Métodos específicos para Person si son necesarios
    }
}
