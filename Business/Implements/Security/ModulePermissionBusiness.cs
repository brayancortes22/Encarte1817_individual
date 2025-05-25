using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.ModulePermission;
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

        /// <summary>
        /// Realiza un borrado lógico de un permiso sobre módulo.
        /// </summary>
        public async Task<bool> DeleteLogicModulePermissionAsync(DeleteLogicalModulePermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del permiso sobre módulo es inválido");

            var exists = await _modulePermissionData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("permiso sobre módulo", dto.Id);

            return await _modulePermissionData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los permisos sobre módulos activos.
        /// </summary>
        public async Task<List<ModulePermissionDto>> GetActiveModulePermissionsAsync()
        {
            try
            {
                var entities = await _modulePermissionData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los permisos sobre módulos activos");
                return _mapper.Map<List<ModulePermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los permisos sobre módulos activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por ID de módulo.
        /// </summary>
        public async Task<List<ModulePermissionDto>> GetByModuleIdAsync(int moduleId)
        {
            if (moduleId <= 0)
                throw new ArgumentException("ID de módulo inválido");

            try
            {
                var entities = await _modulePermissionData.GetByModuleIdAsync(moduleId);
                _logger.LogInformation($"Obteniendo permisos para el módulo con ID: {moduleId}");
                return _mapper.Map<List<ModulePermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del módulo ID {moduleId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un permiso específico por la combinación de módulo y tipo de permiso.
        /// </summary>
        public async Task<ModulePermissionDto> GetByModulePermissionAsync(int moduleId, int permissionId)
        {
            if (moduleId <= 0 || permissionId <= 0)
                throw new ArgumentException("IDs inválidos para la búsqueda");

            try
            {
                var entity = await _modulePermissionData.GetByModulePermissionAsync(moduleId, permissionId);
                _logger.LogInformation($"Buscando permiso específico: Módulo {moduleId}, Permiso {permissionId}");
                return _mapper.Map<ModulePermissionDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar permiso específico: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por ID de tipo de permiso.
        /// </summary>
        public async Task<List<ModulePermissionDto>> GetByPermissionIdAsync(int permissionId)
        {
            if (permissionId <= 0)
                throw new ArgumentException("ID de tipo de permiso inválido");

            try
            {
                var entities = await _modulePermissionData.GetByPermissionIdAsync(permissionId);
                _logger.LogInformation($"Obteniendo permisos del tipo con ID: {permissionId}");
                return _mapper.Map<List<ModulePermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del tipo ID {permissionId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un permiso sobre módulo.
        /// </summary>
        public async Task<bool> UpdatePartialModulePermissionAsync(UpdateModulePermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var modulePermission = _mapper.Map<ModulePermission>(dto);
            return await _modulePermissionData.UpdatePartial(modulePermission);
        }
    }
}
