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
    /// Implementa la lógica de negocio para la gestión de personas.
    /// </summary>
    public class PersonBusiness : BaseBusiness<Person, PersonDto>, IPersonBusiness
    {
        private readonly IPersonData _personData;

        /// <summary>
        /// Constructor de la clase PersonBusiness.
        /// </summary>
        public PersonBusiness(
            IPersonData personData,
            IMapper mapper,
            ILogger<PersonBusiness> logger,
            IGenericIHelpers helpers)
            : base(personData, mapper, logger, helpers)
        {
            _personData = personData;
        }
    }
}
