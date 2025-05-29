using Entity.Dtos;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    /// <summary>
    /// Interface para operaciones de negocio relacionadas con clientes
    /// </summary>
    public interface IClientBusiness : IBaseBusiness<Client, ClientDto>
    {
        // Puedes agregar métodos específicos para la lógica de negocio de clientes si los necesitas
    }
}
