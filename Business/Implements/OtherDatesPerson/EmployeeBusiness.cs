using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
using Entity.Dtos.OtherDatesPerson.Employee;
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
    /// Implementa la lógica de negocio para la gestión de empleados.
    /// </summary>
    public class EmployeeBusiness : BaseBusiness<Employee, EmployeeDto>, IEmployeeBusiness
    {
        private readonly IEmployeeData _employeeData;

        /// <summary>
        /// Constructor de la clase EmployeeBusiness.
        /// </summary>
        public EmployeeBusiness(
            IEmployeeData employeeData,
            IMapper mapper,
            ILogger<EmployeeBusiness> logger,
            IGenericIHelpers helpers)
            : base(employeeData, mapper, logger, helpers)
        {
            _employeeData = employeeData;
        }

        /// <summary>
        /// Realiza un borrado lógico de un empleado.
        /// </summary>
        public async Task<bool> DeleteLogicEmployeeAsync(DeleteLogicalEmployeeDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ValidationException("Id", "El ID del empleado es inválido");

            var exists = await _employeeData.GetByIdAsync(dto.Id)
                ?? throw new EntityNotFoundException("empleado", dto.Id);

            return await _employeeData.ActiveAsync(dto.Id, dto.Status);
        }

        /// <summary>
        /// Obtiene todos los empleados activos.
        /// </summary>
        public async Task<List<EmployeeDto>> GetActiveEmployeesAsync()
        {
            try
            {
                var entities = await _employeeData.GetActiveAsync();
                _logger.LogInformation("Obteniendo todos los empleados activos");
                return _mapper.Map<List<EmployeeDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los empleados activos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene un empleado por su número de documento.
        /// </summary>
        public async Task<EmployeeDto> GetEmployeeByDocumentNumberAsync(string documentNumber)
        {
            if (string.IsNullOrEmpty(documentNumber))
                throw new ArgumentException("Número de documento inválido");

            try
            {
                var entity = await _employeeData.GetByDocumentNumberAsync(documentNumber);
                _logger.LogInformation($"Buscando empleado con número de documento: {documentNumber}");
                return _mapper.Map<EmployeeDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar empleado con documento {documentNumber}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene los empleados por tipo de contrato.
        /// </summary>
        public async Task<List<EmployeeDto>> GetEmployeesByContractTypeAsync(int contractTypeId)
        {
            if (contractTypeId <= 0)
                throw new ArgumentException("ID de tipo de contrato inválido");

            try
            {
                var entities = await _employeeData.GetEmployeesByContractTypeAsync(contractTypeId);
                _logger.LogInformation($"Obteniendo empleados con tipo de contrato ID: {contractTypeId}");
                return _mapper.Map<List<EmployeeDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener empleados con tipo de contrato ID {contractTypeId}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Busca empleados por nombre.
        /// </summary>
        public async Task<List<EmployeeDto>> SearchEmployeesByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nombre inválido para la búsqueda");

            try
            {
                var entities = await _employeeData.SearchEmployeesByNameAsync(name);
                _logger.LogInformation($"Buscando empleados con el nombre: {name}");
                return _mapper.Map<List<EmployeeDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al buscar empleados con el nombre {name}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un empleado.
        /// </summary>
        public async Task<bool> UpdatePartialEmployeeAsync(UpdateEmployeeDto dto)
        {
            if (dto == null || dto.Id <= 0)
                throw new ArgumentException("ID inválido.");

            var employee = _mapper.Map<Employee>(dto);
            return await _employeeData.UpdatePartial(employee);
        }
    }
}
