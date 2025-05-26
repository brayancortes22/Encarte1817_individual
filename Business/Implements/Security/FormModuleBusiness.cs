using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.FormModule;
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

        /// <summary>
        /// Realiza un borrado lógico de una relación formulario-módulo.
        /// </summary>
        public async Task<bool> DeleteLogicFormModuleAsync(DeleteLogicalFormModuleDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID de la relación formulario-módulo es inválido");

            var exists = await _formModuleData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("relación formulario-módulo", dto.Id);

            return await _formModuleData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todas las relaciones formulario-módulo activas.
        /// </summary>
        public async Task<List<FormModuleDto>> GetActiveFormModulesAsync()
        {
            try
            {
                var entities = await _formModuleData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todas las relaciones formulario-módulo activas");
                return _mapper.Map<List<FormModuleDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener las relaciones formulario-módulo activas: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene las relaciones formulario-módulo por ID de formulario.
        /// </summary>
        public async Task<List<FormModuleDto>> GetFormModulesByFormIdAsync(int formId)
        {
            if (formId <= 0)
                throw new ArgumentException("ID de formulario inválido");

            try
            {
                var entities = await _formModuleData.GetFormModulesByFormIdAsync(formId);
                _logger.LogInformation($"Obteniendo relaciones para el formulario con ID: {formId}");
                return _mapper.Map<List<FormModuleDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener relaciones del formulario ID {formId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene las relaciones formulario-módulo por ID de módulo.
        /// </summary>
        public async Task<List<FormModuleDto>> GetFormModulesByModuleIdAsync(int moduleId)
        {
            if (moduleId <= 0)
                throw new ArgumentException("ID de módulo inválido");

            try
            {
                var entities = await _formModuleData.GetFormModulesByModuleIdAsync(moduleId);
                _logger.LogInformation($"Obteniendo relaciones para el módulo con ID: {moduleId}");
                return _mapper.Map<List<FormModuleDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener relaciones del módulo ID {moduleId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente una relación formulario-módulo.
        /// </summary>
        public async Task<bool> UpdatePartialFormModuleAsync(UpdateFormModuleDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var formModule = _mapper.Map<FormModule>(dto);
            return await _formModuleData.UpdatePartial(formModule);
        }
    }
}
