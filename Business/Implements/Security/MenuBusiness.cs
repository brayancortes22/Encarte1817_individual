using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.Menu;
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
    /// Implementa la lógica de negocio para la gestión de menús.
    /// </summary>
    public class MenuBusiness : BaseBusiness<Menu, MenuDto>, IMenuBusiness
    {
        private readonly IMenuData _menuData;

        /// <summary>
        /// Constructor de la clase MenuBusiness.
        /// </summary>
        public MenuBusiness(
            IMenuData menuData,
            IMapper mapper,
            ILogger<MenuBusiness> logger,
            IGenericIHelpers helpers)
            : base(menuData, mapper, logger, helpers)
        {
            _menuData = menuData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un menú.
        /// </summary>
        public async Task<bool> DeleteLogicMenuAsync(DeleteLogicalMenuDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del menú es inválido");

            var exists = await _menuData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("menú", dto.Id);

            return await _menuData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los menús activos.
        /// </summary>
        public async Task<List<MenuDto>> GetActiveMenusAsync()
        {
            try
            {
                var entities = await _menuData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los menús activos");
                return _mapper.Map<List<MenuDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los menús activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los menús asociados a un rol específico.
        /// </summary>
        public async Task<List<MenuDto>> GetMenusByRoleAsync(int roleId)
        {
            if (roleId <= 0)
                throw new ArgumentException("ID de rol inválido");

            try
            {
                var entities = await _menuData.GetMenusByRoleAsync(roleId);
                _logger.LogInformation($"Obteniendo menús para el rol con ID: {roleId}");
                return _mapper.Map<List<MenuDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener menús del rol ID {roleId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los menús hijos de un menú padre específico o los menús raíz si parentId es null.
        /// </summary>
        public async Task<List<MenuDto>> GetMenusByParentIdAsync(int? parentId)
        {
            try
            {
                var entities = await _menuData.GetMenusByParentIdAsync(parentId);
                _logger.LogInformation($"Obteniendo menús con parámetro parentId: {parentId}");
                return _mapper.Map<List<MenuDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener menús con parentId {parentId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un menú.
        /// </summary>
        public async Task<bool> UpdatePartialMenuAsync(UpdateMenuDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var menu = _mapper.Map<Menu>(dto);
            return await _menuData.UpdatePartial(menu);
        }
    }
}
