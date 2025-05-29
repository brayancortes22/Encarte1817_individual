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
    /// Implementa la lógica de negocio para la gestión de departamentos.
    /// </summary>
    public class DepartmentBusiness : BaseBusiness<Department, DepartmentDto>, IDepartmentBusiness
    {
        private readonly IDepartmentData _departmentData;

        /// <summary>
        /// Constructor de la clase DepartmentBusiness.
        /// </summary>        
        public DepartmentBusiness(
            IDepartmentData data,
            IMapper mapper,
            ILogger<DepartmentBusiness> logger,
            IGenericIHelpers helpers)
            : base(data, mapper, logger, helpers)
        {
            _departmentData = data;
        }

      
    }
}
