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
    /// Controlador para la gestión de empleados
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : GenericController<Employee, EmployeeDto>
    {
        private readonly IEmployeeBusiness _employeeBusiness;

        /// <summary>
        /// Constructor del controlador de empleados
        /// </summary>
        /// <param name="employeeBusiness">Servicio de negocio para empleados</param>
        /// <param name="logger">Servicio de logging</param>
        public EmployeeController(
            IEmployeeBusiness employeeBusiness,
            ILogger<EmployeeController> logger)
            : base(employeeBusiness, logger)
        {
            _employeeBusiness = employeeBusiness;
        }

        // Puedes agregar métodos específicos para la entidad Employee aquí si es necesario
    }
}
