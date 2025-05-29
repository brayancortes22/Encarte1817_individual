
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Interfaces;

namespace Business.Implements
{
    /// <summary>
    /// Contiene la lógica de negocio de los métodos específicos para la entidad RolUser.
    /// Extiende BaseBusiness heredando la lógica de negocio de los métodos base.
    /// </summary>
    public class RoleUserBusiness : BaseBusiness<RolUser, RolUserDto>, IRoleUserBusiness
    {
        /// <summary>
        /// Proporciona acceso a los métodos de la capa de datos de roles de usuario.
        /// </summary>
        private readonly IRolUserData _rolUserData;

        /// <summary>
        /// Constructor de la clase RoleUserBusiness.
        /// Inicializa una nueva instancia con las dependencias necesarias para operar con roles de usuario.
        /// </summary>
        /// <param name="rolUserData">Repositorio para acceso a datos de roles de usuario.</param>
        /// <param name="mapper">Servicio de mapeo entre DTOs y entidades.</param>
        /// <param name="logger">Servicio de logging para registro de eventos y errores.</param>
        /// <param name="helpers">Implementación de helpers genéricos utilizados por la capa de negocio.</param>
        public RoleUserBusiness(IRolUserData rolUserData, IMapper mapper, ILogger<RoleUserBusiness> logger, IGenericIHelpers helpers)
           : base(rolUserData, mapper, logger, helpers)
        {
            _rolUserData = rolUserData;
        }

        
    }
}