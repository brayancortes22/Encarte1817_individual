using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con países
    /// </summary>
    public interface ICountryBusiness : IBaseBusiness<Country, CountryDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de países si los necesitas
    }
}
