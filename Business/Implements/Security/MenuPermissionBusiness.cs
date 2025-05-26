using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.MenuPermission;
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
    /// Implementa la lógica de negocio para la gestión de permisos sobre menús.
    /// </summary>
    public class MenuPermissionBusiness : BaseBusiness<MenuPermission, MenuPermissionDto>, IMenuPermissionBusiness
    {
        private readonly IMenuPermissionData _menuPermissionData;

        /// <summary>
        /// Constructor de la clase MenuPermissionBusiness.
        /// </summary>
        public MenuPermissionBusiness(
            IMenuPermissionData menuPermissionData,
            IMapper mapper,
            ILogger<MenuPermissionBusiness> logger,
            IGenericIHelpers helpers)
            : base(menuPermissionData, mapper, logger, helpers)
        {
            _menuPermissionData = menuPermissionData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un permiso sobre menú.
        /// </summary>
        public async Task<bool> DeleteLogicMenuPermissionAsync(DeleteLogicalMenuPermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del permiso sobre menú es inválido");

            var exists = await _menuPermissionData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("permiso sobre menú", dto.Id);

            return await _menuPermissionData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los permisos sobre menús activos.
        /// </summary>
        public async Task<List<MenuPermissionDto>> GetActiveMenuPermissionsAsync()
        {
            try
            {
                var entities = await _menuPermissionData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los permisos sobre menús activos");
                return _mapper.Map<List<MenuPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los permisos sobre menús activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por ID de menú.
        /// </summary>
        public async Task<List<MenuPermissionDto>> GetByMenuIdAsync(int menuId)
        {
            if (menuId <= 0)
                throw new ArgumentException("ID de menú inválido");

            try
            {
                var entities = await _menuPermissionData.GetByMenuIdAsync(menuId);
                _logger.LogInformation($"Obteniendo permisos para el menú con ID: {menuId}");
                return _mapper.Map<List<MenuPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del menú ID {menuId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un permiso específico por la combinación de menú y tipo de permiso.
        /// </summary>
        public async Task<MenuPermissionDto> GetByMenuPermissionAsync(int menuId, int permissionId)
        {
            if (menuId <= 0 || permissionId <= 0)
                throw new ArgumentException("IDs inválidos para la búsqueda");

            try
            {
                var entity = await _menuPermissionData.GetByMenuPermissionAsync(menuId, permissionId);
                _logger.LogInformation($"Buscando permiso específico: Menú {menuId}, Permiso {permissionId}");
                return _mapper.Map<MenuPermissionDto>(entity);
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
        public async Task<List<MenuPermissionDto>> GetByPermissionIdAsync(int permissionId)
        {
            if (permissionId <= 0)
                throw new ArgumentException("ID de tipo de permiso inválido");

            try
            {
                var entities = await _menuPermissionData.GetByPermissionIdAsync(permissionId);
                _logger.LogInformation($"Obteniendo permisos del tipo con ID: {permissionId}");
                return _mapper.Map<List<MenuPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del tipo ID {permissionId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un permiso sobre menú.
        /// </summary>
        public async Task<bool> UpdatePartialMenuPermissionAsync(UpdateMenuPermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var menuPermission = _mapper.Map<MenuPermission>(dto);
            return await _menuPermissionData.UpdatePartial(menuPermission);
        }
    }
}
