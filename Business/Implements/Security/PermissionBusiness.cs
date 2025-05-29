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
    /// Implementa la lógica de negocio para la gestión de permisos.
    /// </summary>
    public class PermissionBusiness : BaseBusiness<Permission, PermissionDto>, IPermissionBusiness
    {
        private readonly IPermissionData _permissionData;

        /// <summary>
        /// Constructor de la clase PermissionBusiness.
        /// </summary>
        public PermissionBusiness(
            IPermissionData permissionData,
            IMapper mapper,
            ILogger<PermissionBusiness> logger,
            IGenericIHelpers helpers)
            : base(permissionData, mapper, logger, helpers)
        {
            _permissionData = permissionData;
        }
    }
}
