using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.Module;
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

        /// <summary>
        /// Realiza un borrado lógico de un módulo.
        /// </summary>
        public async Task<bool> DeleteLogicModuleAsync(DeleteLogicalModuleDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del módulo es inválido");

            var exists = await _moduleData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("módulo", dto.Id);

            return await _moduleData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los módulos activos.
        /// </summary>
        public async Task<List<ModuleDto>> GetActiveModulesAsync()
        {
            try
            {
                var entities = await _moduleData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los módulos activos");
                return _mapper.Map<List<ModuleDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los módulos activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los módulos asociados a un rol específico.
        /// </summary>
        public async Task<List<ModuleDto>> GetModulesByRoleAsync(int roleId)
        {
            if (roleId <= 0)
                throw new ArgumentException("ID de rol inválido");

            try
            {
                var entities = await _moduleData.GetModulesByRoleAsync(roleId);
                _logger.LogInformation($"Obteniendo módulos para el rol con ID: {roleId}");
                return _mapper.Map<List<ModuleDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener módulos del rol ID {roleId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un módulo.
        /// </summary>
        public async Task<bool> UpdatePartialModuleAsync(UpdateModuleDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var module = _mapper.Map<Module>(dto);
            return await _moduleData.UpdatePartial(module);
        }
    }
}
