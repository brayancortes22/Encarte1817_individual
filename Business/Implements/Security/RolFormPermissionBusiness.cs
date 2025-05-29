using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using Utilities.Interfaces;

namespace Business.Implements
{
    /// <summary>
    /// Implementa la lógica de negocio para la gestión de permisos de roles sobre formularios.
    /// </summary>
    public class RolFormPermissionBusiness : BaseBusiness<RolFormPermission, RolFormPermissionDto>, IRolFormPermissionBusiness
    {
        private readonly IRolFormPermissionData _rolFormPermissionData;

        /// <summary>
        /// Constructor de la clase RolFormPermissionBusiness.
        /// </summary>
        public RolFormPermissionBusiness(
            IRolFormPermissionData rolFormPermissionData,
            IMapper mapper,
            ILogger<RolFormPermissionBusiness> logger,
            IGenericIHelpers helpers)
            : base(rolFormPermissionData, mapper, logger, helpers)
        {
            _rolFormPermissionData = rolFormPermissionData;
        }

        
    }
}
