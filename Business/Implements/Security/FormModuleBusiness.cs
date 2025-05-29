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
    /// Implementa la lógica de negocio para la gestión de relaciones entre formularios y módulos.
    /// </summary>
    public class FormModuleBusiness : BaseBusiness<FormModule, FormModuleDto>, IFormModuleBusiness
    {
        private readonly IFormModuleData _formModuleData;

        /// <summary>
        /// Constructor de la clase FormModuleBusiness.
        /// </summary>
        public FormModuleBusiness(
            IFormModuleData formModuleData,
            IMapper mapper,
            ILogger<FormModuleBusiness> logger,
            IGenericIHelpers helpers)
            : base(formModuleData, mapper, logger, helpers)
        {
            _formModuleData = formModuleData;
        }

    }
}
