using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.Permission;
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

        /// <summary>
        /// Realiza un borrado lógico de un permiso.
        /// </summary>
        public async Task<bool> DeleteLogicPermissionAsync(DeleteLogicalPermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del permiso es inválido");

            var exists = await _permissionData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("permiso", dto.Id);

            return await _permissionData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los permisos activos.
        /// </summary>
        public async Task<List<PermissionDto>> GetActivePermissionsAsync()
        {
            try
            {
                var entities = await _permissionData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los permisos activos");
                return _mapper.Map<List<PermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los permisos activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por tipo.
        /// </summary>
        public async Task<List<PermissionDto>> GetPermissionsByTypeAsync(int permissionTypeId)
        {
            try
            {
                var entities = await _permissionData.GetByTypeAsync(permissionTypeId);
                _logger.LogInformation($"Obteniendo permisos del tipo: {permissionTypeId}");
                return _mapper.Map<List<PermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del tipo {permissionTypeId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un permiso.
        /// </summary>
        public async Task<bool> UpdatePartialPermissionAsync(UpdatePermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var permission = _mapper.Map<Permission>(dto);
            return await _permissionData.UpdatePartial(permission);
        }
    }
}
