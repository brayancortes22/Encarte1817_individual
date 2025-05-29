using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con proveedores
    /// </summary>
    public interface IProviderBusiness : IBaseBusiness<Provider, ProviderDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de proveedores si los necesitas
    }
}
