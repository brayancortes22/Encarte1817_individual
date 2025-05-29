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
    /// Implementación de operaciones de datos para entidades Country
    /// </summary>
    public class CountryData : BaseModelData<Country>, ICountryData
    {
        public CountryData(ApplicationDbContext context) : base(context)
        {
        }
        
        // Si necesitas sobreescribir métodos o agregar funcionalidad adicional, puedes hacerlo aquí
    }
}
