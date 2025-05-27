# Sistema de Auditoría para Entity Framework Core

## Introducción

Este proyecto implementa un sistema de auditoría avanzado para Entity Framework Core, separando los datos operativos de los registros de auditoría. La arquitectura permite mantener la base de datos principal limpia y optimizada, mientras registra detalladamente todos los cambios en una base de datos secundaria.

## Estructura del Sistema de Auditoría

```
Entity/
├── Audit/
│   └── AuditEntry.cs        # Clase auxiliar para capturar cambios
├── Context/
│   ├── ApplicationDbContext.cs  # Contexto principal de la aplicación
│   └── LogDbContext.cs      # Contexto para la base de datos de logs
├── Model/
│   ├── Base/
│   │   └── BaseEntity.cs    # Clase base para todas las entidades
│   └── Security/
│       └── ChangeLog.cs     # Modelo para registrar los cambios
```

## Componentes Principales

### 1. BaseEntity

La clase base para todas las entidades del sistema. Diseñada para ser minimalista, solo incluye:

```csharp
public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool Status { get; set; } = true;
    
    // Solo mantenemos el estado activo/inactivo en la entidad principal
    // El resto de información de auditoría irá solo en la base de logs
}
```

**¿Por qué?** Para mantener las tablas principales limpias y eficientes, sin sobrecarga de campos de auditoría.

### 2. ChangeLog

Modelo para registrar todos los cambios realizados en el sistema:

```csharp
public class ChangeLog
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int IdTable { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string OldValues { get; set; } = string.Empty;
    public string NewValues { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty; // Insert, Update, Delete
    public bool Active { get; set; } = true;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public string EntityName { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
}
```

**¿Por qué?** Para capturar información detallada sobre cada cambio, incluyendo quién lo hizo, cuándo, desde dónde, y exactamente qué cambió.

### 3. ApplicationDbContext

El contexto principal que interactúa con la base de datos operativa. Sus características principales incluyen:

- Definición de `DbSet<T>` para todas las entidades
- Configuración de relaciones entre entidades
- Sobrescritura de métodos `SaveChanges` para implementar auditoría
- Conversión automática de borrados físicos a borrados lógicos

**¿Por qué?** Para interceptar y procesar los cambios antes de que se guarden en la base de datos.

### 4. LogDbContext

Contexto separado para manejar la base de datos de logs:

```csharp
public class LogDbContext : DbContext
{
    public DbSet<ChangeLog> ChangeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChangeLog>(entity =>
        {
            entity.ToTable("ChangeLogs");
            entity.HasKey(e => e.Id);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}
```

**¿Por qué?** Para mantener completamente separada la base de datos de logs, permitiendo políticas de seguridad y retención diferentes.

## Funcionamiento del Sistema de Auditoría

### 1. Captura de Cambios

Cuando se llama a `SaveChanges()`, el proceso es:

1. Se detectan las entidades modificadas (agregadas, actualizadas o eliminadas)
2. Se captura el estado anterior y nuevo de cada propiedad
3. Los borrados físicos se convierten en borrados lógicos (Status = false)
4. Se obtiene información del usuario actual y su dirección IP
5. Se crea un registro `AuditEntry` para cada cambio

### 2. Almacenamiento de Logs

Después de guardar los cambios en la base de datos principal:

1. Se crea un objeto `ChangeLog` para cada `AuditEntry`
2. Los valores anteriores y nuevos se serializan a JSON
3. El log se guarda en la base de datos secundaria
4. Los errores en el proceso de logging no afectan la operación principal

## Configuración

El sistema utiliza dos cadenas de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Encarte1817_v2;...",
  "LogConnection": "Server=localhost;Database=Encarte1817_v2_Logs;..."
}
```

Y se registran ambos contextos en `Program.cs`:

```csharp
// Registrar ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar LogDbContext
builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogConnection")));

// Necesario para capturar el usuario actual
builder.Services.AddHttpContextAccessor();
```

## Beneficios

1. **Separación de preocupaciones**: Base de datos principal limpia, toda la auditoría en base separada
2. **Rendimiento**: Consultas más rápidas en la base de datos principal
3. **Escalabilidad**: Las bases de datos pueden escalar independientemente
4. **Cumplimiento**: Registro detallado para auditoría y cumplimiento normativo
5. **Seguridad**: Trazabilidad completa de quién hizo qué y cuándo

## Migraciones

Para crear las bases de datos:

```powershell
# Para la base de datos principal
dotnet ef migrations add InitialMigration -c ApplicationDbContext -o Migrations/ApplicationDb
dotnet ef database update -c ApplicationDbContext

# Para la base de datos de logs
dotnet ef migrations add InitialLogMigration -c LogDbContext -o Migrations/LogDb
dotnet ef database update -c LogDbContext
```

## Consideraciones Adicionales

- El sistema está diseñado para ser no intrusivo - los errores en el logging no afectan las operaciones principales
- Se puede configurar la retención de logs independientemente de los datos principales
- Es posible implementar políticas de backup diferentes para cada base de datos
