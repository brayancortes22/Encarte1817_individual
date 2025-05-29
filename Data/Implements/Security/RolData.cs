using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implements
{
    /// <summary>
    /// Implementación de operaciones de datos para entidades Rol (rol)
    /// </summary>
    public class RolData : BaseModelData<Rol>, IRolData
    {
        public RolData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para Rol si es necesario
    }
}
