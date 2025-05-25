using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.Department;
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
    /// Implementa la lógica de negocio para la gestión de departamentos.
    /// </summary>
    public class DepartmentBusiness : BaseBusiness<Department, DepartmentDto>, IDepartmentBusiness
    {
        private readonly IDepartmentData _departmentData;

        /// <summary>
        /// Constructor de la clase DepartmentBusiness.
        /// </summary>
        public DepartmentBusiness(
            IDepartmentData departmentData,
            IMapper mapper,
            ILogger<DepartmentBusiness> logger,
            IGenericIHelpers helpers)
            : base(departmentData, mapper, logger, helpers)
        {
            _departmentData = departmentData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un departamento.
        /// </summary>
        public async Task<bool> DeleteLogicDepartmentAsync(DeleteLogicalDepartmentDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del departamento es inválido");

            var exists = await _departmentData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("departamento", dto.Id);

            return await _departmentData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los departamentos activos.
        /// </summary>
        public async Task<List<DepartmentDto>> GetActiveDepartmentsAsync()
        {
            try
            {
                var entities = await _departmentData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los departamentos activos");
                return _mapper.Map<List<DepartmentDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los departamentos activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los departamentos de un país específico.
        /// </summary>
        public async Task<List<DepartmentDto>> GetDepartmentsByCountryAsync(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("ID de país inválido");

            try
            {
                var entities = await _departmentData.GetDepartmentsByCountryAsync(countryId);
                _logger.LogInformation($"Obteniendo departamentos del país con ID: {countryId}");
                return _mapper.Map<List<DepartmentDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener departamentos del país ID {countryId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un departamento.
        /// </summary>
        public async Task<bool> UpdatePartialDepartmentAsync(UpdateDepartmentDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var department = _mapper.Map<Department>(dto);
            return await _departmentData.UpdatePartial(department);
        }
    }
}
