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
    /// Controlador para la gestión de departamentos/estados
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : GenericController<Department, DepartmentDto>
    {
        private readonly IDepartmentBusiness _departmentBusiness;

        /// <summary>
        /// Constructor del controlador de departamentos
        /// </summary>
        /// <param name="departmentBusiness">Servicio de negocio para departamentos</param>
        /// <param name="logger">Servicio de logging</param>
        public DepartmentController(
            IDepartmentBusiness departmentBusiness,
            ILogger<DepartmentController> logger)
            : base(departmentBusiness, logger)
        {
            _departmentBusiness = departmentBusiness;
        }

        // Puedes agregar métodos específicos para la entidad Department aquí si es necesario
    }
}
