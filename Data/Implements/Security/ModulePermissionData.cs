using Data.Implements.BaseData;
using Data.Interfaces;
using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implements
{
    /// <summary>
    /// Implementación de operaciones de datos para entidades ModulePermission (permisos por módulo)
    /// </summary>
    public class ModulePermissionData : BaseModelData<ModulePermission>, IModulePermissionData
    {
        public ModulePermissionData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para ModulePermission si es necesario
    }
}
