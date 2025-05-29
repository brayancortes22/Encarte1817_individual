using AutoMapper;
using Microsoft.Extensions.Logging;
using Entity.Model;
using Entity.Dtos;
using Business.Interfaces;
using Data.Interfaces;
using Utilities.Exceptions;
using ValidationException = Utilities.Exceptions.ValidationException;
using Utilities.Interfaces;


namespace Business.Implements
{
    /// <summary>
    /// Contiene la logica de negocio de los metodos especificos para la entidad Rol
    /// Extiende BaseBusiness heredando la logica de negocio de los metodos base 
    /// </summary>
    public class RolBusiness : BaseBusiness<Rol, RolDto>, IRolBusiness
    {
        ///<summary>Proporciona acceso a los metodos de la capa de datos de roles</summary>
        private readonly IRolData _rolData;

        /// <summary>
        /// Constructor de la clase RolBusiness
        /// Inicializa una nueva instancia con las dependencias necesarias para operar con roles.
        /// </summary>
        public RolBusiness(IRolData rolData, IMapper mapper, ILogger<RolBusiness> logger, IGenericIHelpers helpers)
      : base(rolData, mapper, logger, helpers)
        {
            _rolData = rolData;
        }

    }
}