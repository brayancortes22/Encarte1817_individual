using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements.Security
{
    /// <summary>
    /// Implementación de operaciones de datos para entidades Module (módulo)
    /// </summary>
    public class ModuleData : BaseModelData<Module>, IModuleData
    {
        public ModuleData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para Module si es necesario
    }
}
