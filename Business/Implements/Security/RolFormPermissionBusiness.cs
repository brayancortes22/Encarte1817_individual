using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.Security.RolFormPermission;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using Utilities.Interfaces;

namespace Business.Implements
{
    /// <summary>
    /// Implementa la lógica de negocio para la gestión de permisos de roles sobre formularios.
    /// </summary>
    public class RolFormPermissionBusiness : BaseBusiness<RolFormPermission, RolFormPermissionDto>, IRolFormPermissionBusiness
    {
        private readonly IRolFormPermissionData _rolFormPermissionData;

        /// <summary>
        /// Constructor de la clase RolFormPermissionBusiness.
        /// </summary>
        public RolFormPermissionBusiness(
            IRolFormPermissionData rolFormPermissionData,
            IMapper mapper,
            ILogger<RolFormPermissionBusiness> logger,
            IGenericIHelpers helpers)
            : base(rolFormPermissionData, mapper, logger, helpers)
        {
            _rolFormPermissionData = rolFormPermissionData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un permiso de rol sobre formulario.
        /// </summary>
        public async Task<bool> DeleteLogicRolFormPermissionAsync(DeleteLogicalRolFormPermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del permiso de rol sobre formulario es inválido");

            var exists = await _rolFormPermissionData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("permiso de rol sobre formulario", dto.Id);

            return await _rolFormPermissionData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los permisos de roles sobre formularios activos.
        /// </summary>
        public async Task<List<RolFormPermissionDto>> GetActiveRolFormPermissionsAsync()
        {
            try
            {
                var entities = await _rolFormPermissionData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los permisos de roles sobre formularios activos");
                return _mapper.Map<List<RolFormPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los permisos de roles sobre formularios activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por ID de formulario.
        /// </summary>
        public async Task<List<RolFormPermissionDto>> GetByFormIdAsync(int formId)
        {
            if (formId <= 0)
                throw new ArgumentException("ID de formulario inválido");

            try
            {
                var entities = await _rolFormPermissionData.GetByFormIdAsync(formId);
                _logger.LogInformation($"Obteniendo permisos para el formulario con ID: {formId}");
                return _mapper.Map<List<RolFormPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del formulario ID {formId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por ID de tipo de permiso.
        /// </summary>
        public async Task<List<RolFormPermissionDto>> GetByPermissionIdAsync(int permissionId)
        {
            if (permissionId <= 0)
                throw new ArgumentException("ID de tipo de permiso inválido");

            try
            {
                var entities = await _rolFormPermissionData.GetByPermissionIdAsync(permissionId);
                _logger.LogInformation($"Obteniendo permisos del tipo con ID: {permissionId}");
                return _mapper.Map<List<RolFormPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del tipo ID {permissionId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un permiso específico por la combinación de rol, formulario y tipo de permiso.
        /// </summary>
        public async Task<RolFormPermissionDto> GetByRolFormPermissionAsync(int rolId, int formId, int permissionId)
        {
            if (rolId <= 0 || formId <= 0 || permissionId <= 0)
                throw new ArgumentException("IDs inválidos para la búsqueda");

            try
            {
                var entity = await _rolFormPermissionData.GetByRolFormPermissionAsync(rolId, formId, permissionId);
                _logger.LogInformation($"Buscando permiso específico: Rol {rolId}, Form {formId}, Permission {permissionId}");
                return _mapper.Map<RolFormPermissionDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar permiso específico: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los permisos por ID de rol.
        /// </summary>
        public async Task<List<RolFormPermissionDto>> GetByRolIdAsync(int rolId)
        {
            if (rolId <= 0)
                throw new ArgumentException("ID de rol inválido");

            try
            {
                var entities = await _rolFormPermissionData.GetByRolIdAsync(rolId);
                _logger.LogInformation($"Obteniendo permisos para el rol con ID: {rolId}");
                return _mapper.Map<List<RolFormPermissionDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener permisos del rol ID {rolId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un permiso de rol sobre formulario.
        /// </summary>
        public async Task<bool> UpdatePartialRolFormPermissionAsync(UpdateRolFormPermissionDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var rolFormPermission = _mapper.Map<RolFormPermission>(dto);
            return await _rolFormPermissionData.UpdatePartial(rolFormPermission);
        }
    }
}
