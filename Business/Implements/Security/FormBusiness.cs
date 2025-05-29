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
    /// Implementa la lógica de negocio para la gestión de formularios.
    /// </summary>
    public class FormBusiness : BaseBusiness<Form, FormDto>, IFormBusiness
    {
        private readonly IFormData _formData;

        /// <summary>
        /// Constructor de la clase FormBusiness.
        /// </summary>
        public FormBusiness(
            IFormData formData,
            IMapper mapper,
            ILogger<FormBusiness> logger,
            IGenericIHelpers helpers)
            : base(formData, mapper, logger, helpers)
        {
            _formData = formData;
        }

    }
}
