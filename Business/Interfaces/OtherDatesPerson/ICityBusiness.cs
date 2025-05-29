using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con ciudades
    /// </summary>
    public interface ICityBusiness : IBaseBusiness<City, CityDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de ciudades si los necesitas
    }
}
