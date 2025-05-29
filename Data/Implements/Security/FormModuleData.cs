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
    /// Implementación de operaciones de datos para entidades FormModule (relación formulario-módulo)
    /// </summary>
    public class FormModuleData : BaseModelData<FormModule>, IFormModuleData
    {
        public FormModuleData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para FormModule si es necesario
    }
}
