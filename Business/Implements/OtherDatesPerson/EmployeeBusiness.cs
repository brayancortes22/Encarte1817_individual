using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Exceptions;
using Utilities.Interfaces;

namespace Business.Implements
{
    /// <summary>
    /// Implementa la lógica de negocio para la gestión de empleados.
    /// </summary>
    public class EmployeeBusiness : BaseBusiness<Employee, EmployeeDto>, IEmployeeBusiness
    {
        private readonly IEmployeeData _employeeData;

        /// <summary>
        /// Constructor de la clase EmployeeBusiness.
        /// </summary>
        public EmployeeBusiness(
            IEmployeeData employeeData,
            IMapper mapper,
            ILogger<EmployeeBusiness> logger,
            IGenericIHelpers helpers)
            : base(employeeData, mapper, logger, helpers)
        {
            _employeeData = employeeData;
        }

    }
}
