using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con distritos/localidades
    /// </summary>
    public interface IDistrictBusiness : IBaseBusiness<District, DistrictDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de distritos si los necesitas
    }
}
