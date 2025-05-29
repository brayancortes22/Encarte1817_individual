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
    /// Controlador para la gestión de países
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : GenericController<Country, CountryDto>
    {
        private readonly ICountryBusiness _countryBusiness;

        /// <summary>
        /// Constructor del controlador de países
        /// </summary>
        /// <param name="countryBusiness">Servicio de negocio para países</param>
        /// <param name="logger">Servicio de logging</param>
        public CountryController(
            ICountryBusiness countryBusiness,
            ILogger<CountryController> logger)
            : base(countryBusiness, logger)
        {
            _countryBusiness = countryBusiness;
        }

        // Puedes agregar métodos específicos para la entidad Country aquí si es necesario
        // Por ejemplo:
        //
        // [HttpGet("by-region/{region}")]
        // public async Task<ActionResult<IEnumerable<CountryDto>>> GetByRegion(string region)
        // {
        //     try
        //     {
        //         var results = await _countryBusiness.GetCountriesByRegionAsync(region);
        //         return Ok(results);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, $"Error al obtener países de la región {region}");
        //         return StatusCode(500, new { message = "Error interno del servidor" });
        //     }
        // }
    }
}
