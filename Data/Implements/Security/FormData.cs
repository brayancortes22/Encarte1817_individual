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
    /// Implementación de operaciones de datos para entidades Form (formulario)
    /// </summary>
    public class FormData : BaseModelData<Form>, IFormData
    {
        public FormData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Aquí puedes agregar métodos específicos para Form si es necesario
    }
}
