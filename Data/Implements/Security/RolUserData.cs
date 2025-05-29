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
    /// Implementación de operaciones de datos para entidades RolUser (relación rol-usuario)
    /// </summary>
    public class RolUserData : BaseModelData<RolUser>, IRolUserData
    {
        public RolUserData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para RolUser si es necesario
    }
}