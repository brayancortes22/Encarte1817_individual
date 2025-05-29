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
    /// Implementa la lógica de negocio para la gestión de permisos sobre módulos.
    /// </summary>
    public class ModulePermissionBusiness : BaseBusiness<ModulePermission, ModulePermissionDto>, IModulePermissionBusiness
    {
        private readonly IModulePermissionData _modulePermissionData;

        /// <summary>
        /// Constructor de la clase ModulePermissionBusiness.
        /// </summary>
        public ModulePermissionBusiness(
            IModulePermissionData modulePermissionData,
            IMapper mapper,
            ILogger<ModulePermissionBusiness> logger,
            IGenericIHelpers helpers)
            : base(modulePermissionData, mapper, logger, helpers)
        {
            _modulePermissionData = modulePermissionData;
        }

        
    }
}
