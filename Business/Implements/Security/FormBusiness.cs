using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.Form;
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

        /// <summary>
        /// Realiza un borrado lógico de un formulario.
        /// </summary>
        public async Task<bool> DeleteLogicFormAsync(DeleteLogicalFormDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del formulario es inválido");

            var exists = await _formData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("formulario", dto.Id);

            return await _formData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los formularios activos.
        /// </summary>
        public async Task<List<FormDto>> GetActiveFormsAsync()
        {
            try
            {
                var entities = await _formData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los formularios activos");
                return _mapper.Map<List<FormDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los formularios activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los formularios asociados a un módulo específico.
        /// </summary>
        public async Task<List<FormDto>> GetFormsByModuleAsync(int moduleId)
        {
            if (moduleId <= 0)
                throw new ArgumentException("ID de módulo inválido");

            try
            {
                var entities = await _formData.GetFormsByModuleAsync(moduleId);
                _logger.LogInformation($"Obteniendo formularios para el módulo con ID: {moduleId}");
                return _mapper.Map<List<FormDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener formularios del módulo ID {moduleId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un formulario.
        /// </summary>
        public async Task<bool> UpdatePartialFormAsync(UpdateFormDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var form = _mapper.Map<Form>(dto);
            return await _formData.UpdatePartial(form);
        }
    }
}
