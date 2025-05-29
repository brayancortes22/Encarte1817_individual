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
    /// Implementa la lógica de negocio para la gestión de módulos.
    /// </summary>
    public class ModuleBusiness : BaseBusiness<Module, ModuleDto>, IModuleBusiness
    {
        private readonly IModuleData _moduleData;

        /// <summary>
        /// Constructor de la clase ModuleBusiness.
        /// </summary>
        public ModuleBusiness(
            IModuleData moduleData,
            IMapper mapper,
            ILogger<ModuleBusiness> logger,
            IGenericIHelpers helpers)
            : base(moduleData, mapper, logger, helpers)
        {
            _moduleData = moduleData;
        }
    }
}
