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
    /// Controlador para la gestión de ciudades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CityController : GenericController<City, CityDto>
    {
        private readonly ICityBusiness _cityBusiness;

        /// <summary>
        /// Constructor del controlador de ciudades
        /// </summary>
        /// <param name="cityBusiness">Servicio de negocio para ciudades</param>
        /// <param name="logger">Servicio de logging</param>
        public CityController(
            ICityBusiness cityBusiness,
            ILogger<CityController> logger)
            : base(cityBusiness, logger)
        {
            _cityBusiness = cityBusiness;
        }

        // Puedes agregar métodos específicos para la entidad City aquí si es necesario
    }
}
