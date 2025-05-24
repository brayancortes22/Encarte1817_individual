using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enums
{
    /// <summary>
    /// Tipos de permisos disponibles en el sistema
    /// </summary>
    public enum PermissionType
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4,
        Export = 5,
        Import = 6,
        Approve = 7,
        Reject = 8,
        Print = 9,
        All = 99
    }
}
