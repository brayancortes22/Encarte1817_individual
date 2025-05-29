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
    /// Controlador para la gestión de distritos/localidades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DistrictController : GenericController<District, DistrictDto>
    {
        private readonly IDistrictBusiness _districtBusiness;

        /// <summary>
        /// Constructor del controlador de distritos
        /// </summary>
        /// <param name="districtBusiness">Servicio de negocio para distritos</param>
        /// <param name="logger">Servicio de logging</param>
        public DistrictController(
            IDistrictBusiness districtBusiness,
            ILogger<DistrictController> logger)
            : base(districtBusiness, logger)
        {
            _districtBusiness = districtBusiness;
        }

        // Puedes agregar métodos específicos para la entidad District aquí si es necesario
    }
}
