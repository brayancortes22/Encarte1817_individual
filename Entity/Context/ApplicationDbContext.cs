using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Http; 
using Entity.Model;
using Entity.Model.Base;
using Entity.Audit;
using System.Data;
using Dapper;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entity.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, 
            IConfiguration configuration,
           
            IHttpContextAccessor httpContextAccessor = null) : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        //Dbset SETS - Security
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolUser> RolUsers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<FormModule> FormModules { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolFormPermission> RolFormPermissions { get; set; }
        public DbSet<ModulePermission> ModulePermissions { get; set; }
       
        //Dbset SETS - OtherDatesPerson
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Provider> Providers { get; set; }
        
        // Relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación User-Rol (muchos-a-muchos)
            modelBuilder.Entity<RolUser>()
                .HasKey(ru => new { ru.UserId, ru.RolId }); // Clave compuesta

            modelBuilder.Entity<RolUser>()
                .HasOne(ru => ru.User)
                .WithMany(u => u.RolUsers)
                .HasForeignKey(ru => ru.UserId);

            modelBuilder.Entity<RolUser>()
                .HasOne(ru => ru.Rol)
                .WithMany(r => r.RolUsers)
                .HasForeignKey(ru => ru.RolId);

            // Configuración de la relación Rol-Form-Permission (muchos-a-muchos ternaria)
            modelBuilder.Entity<RolFormPermission>()
                .HasOne(rfp => rfp.Rol)
                .WithMany(r => r.RolFormPermissions)
                .HasForeignKey(rfp => rfp.RolId);

            modelBuilder.Entity<RolFormPermission>()
                .HasOne(rfp => rfp.Form)
                .WithMany(f => f.RolFormPermissions)
                .HasForeignKey(rfp => rfp.FormId);

            modelBuilder.Entity<RolFormPermission>()
                .HasOne(rfp => rfp.Permission)
                .WithMany(p => p.RolFormPermissions)
                .HasForeignKey(rfp => rfp.PermissionId);

            // Configuración de Form-Module
            modelBuilder.Entity<FormModule>()
                .HasOne(fm => fm.Form)
                .WithMany()
                .HasForeignKey(fm => fm.FormId);

            modelBuilder.Entity<FormModule>()
                .HasOne(fm => fm.Module)
                .WithMany()
                .HasForeignKey(fm => fm.ModuleId);

            // Configuración de Module-Permission
            modelBuilder.Entity<ModulePermission>()
                .HasOne(mp => mp.Module)
                .WithMany(m => m.ModulePermissions)
                .HasForeignKey(mp => mp.ModuleId);

            modelBuilder.Entity<ModulePermission>()
                .HasOne(mp => mp.Permission)
                .WithMany()
                .HasForeignKey(mp => mp.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuración para City-Department
            modelBuilder.Entity<City>()
                .HasOne(c => c.Department)
                .WithMany()
                .HasForeignKey("DepartmentId");

            // Configuración para Department-Country
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Country)
                .WithMany()
                .HasForeignKey("CountryId");

            // Configuración para todas las entidades que heredan de BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(BaseEntity))))
            {
                // Configurar Status con un valor predeterminado de true
                modelBuilder.Entity(entityType.ClrType)
                    .Property("Status")
                    .HasDefaultValue(true);
            }

            // Configuración para Employee -person (uno a uno)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.person)
                .WithMany()
                .HasForeignKey(e => e.PersonId);
                // Mantenemos ON DELETE CASCADE

            // Configuración para User-Person (uno a uno)
            modelBuilder.Entity<User>()
                .HasOne<Person>() // Un usuario tiene una persona
                .WithOne(p => p.User) // Una persona tiene un usuario
                .HasForeignKey<User>(u => u.PersonId) // La clave foránea está en User
                .OnDelete(DeleteBehavior.NoAction); // Usar NoAction para evitar ciclos de cascada

            // Configuración para Client-Person (uno a uno)
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Person)
                .WithMany()
                .HasForeignKey(c => c.PersonId);

            // Configuración para Provider-Person (uno a uno)
            modelBuilder.Entity<Provider>()
                .HasOne(p => p.person)
                .WithMany()
                .HasForeignKey(p => p.PersonId);

            // Configuración para District-City (muchos a uno)
            modelBuilder.Entity<District>()
                .HasOne(d => d.City)
                .WithMany()
                .HasForeignKey("CityId");
        }

        /// <summary>
        /// Configura opciones adicionales del contexto, como el registro de datos sensibles.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // Otras configuraciones adicionales pueden ir aquí
        }

        /// <summary>
        /// Configura convenciones de tipos de datos, estableciendo la precisión por defecto de los valores decimales.
        /// </summary>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        /// <summary>
        /// Guarda los cambios en la base de datos, asegurando la auditoría antes de persistir los datos.
        /// </summary>
        public override int SaveChanges()
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            OnAfterSaveChanges(auditEntries);
            return result;
        }

        /// <summary>
        /// Guarda los cambios en la base de datos de manera asíncrona, asegurando la auditoría antes de la persistencia.
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity && entry.State != EntityState.Detached && entry.State != EntityState.Unchanged)
                {
                    var auditEntry = new AuditEntry(entry);
                    auditEntry.TableName = entry.Metadata.GetTableName();
                    auditEntry.EntityName = entry.Entity.GetType().Name;
                    auditEntry.Action = entry.State.ToString();
                    
                    // Get current user info
                    string userName = "System";
                    string ipAddress = "::1";
                    
                    if (_httpContextAccessor?.HttpContext != null)
                    {
                        var user = _httpContextAccessor.HttpContext.User;
                        userName = user?.Identity?.Name ?? "Anonymous";
                        ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "::1";
                    }
                    
                    auditEntry.UserName = userName;
                    auditEntry.IpAddress = ipAddress;
                    
                    // Get entity ID
                    if (entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Id")?.CurrentValue is int id)
                    {
                        auditEntry.IdTable = id;
                    }
                    
                    // Handle soft delete for BaseEntity
                    if (entry.State == EntityState.Deleted && entry.Entity is BaseEntity entity)
                    {
                        entry.State = EntityState.Modified;
                        entity.Status = false;
                    }
                    
                    // Capture property values
                    foreach (var property in entry.Properties)
                    {
                        if (property.IsTemporary)
                        {
                            // Skip temporary properties
                            continue;
                        }
                        
                        string propertyName = property.Metadata.Name;
                        
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[propertyName] = property.CurrentValue;
                            continue;
                        }
                        
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                break;
                            
                            case EntityState.Deleted:
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                break;
                            
                            case EntityState.Modified:
                                if (property.IsModified)
                                {
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }
                    }
                    
                    auditEntries.Add(auditEntry);
                }
            }
            
            return auditEntries;
        }

        private void OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            try
            {
                using var logContext = new LogDbContext(
                    new DbContextOptionsBuilder<LogDbContext>()
                        .UseSqlServer(_configuration.GetConnectionString("LogConnection"))
                        .Options);

                foreach (var auditEntry in auditEntries)
                {
                    // Create log entity
                    var log = new ChangeLog
                    {
                        UserName = auditEntry.UserName,
                        IdTable = auditEntry.IdTable,
                        TableName = auditEntry.TableName,
                        Action = auditEntry.Action,
                        EntityName = auditEntry.EntityName,
                        IpAddress = auditEntry.IpAddress,
                        CreateAt = DateTime.UtcNow,
                        OldValues = JsonSerializer.Serialize(auditEntry.OldValues),
                        NewValues = JsonSerializer.Serialize(auditEntry.NewValues)
                    };
                    
                    logContext.ChangeLogs.Add(log);
                }

                logContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log error but don't throw, we don't want audit to break business operations
                // You might want to log this to a file or other logging service
                Console.WriteLine($"Error saving audit logs: {ex.Message}");
            }
        }

        private async Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return;

            try
            {
                using var logContext = new LogDbContext(
                    new DbContextOptionsBuilder<LogDbContext>()
                        .UseSqlServer(_configuration.GetConnectionString("LogConnection"))
                        .Options);

                foreach (var auditEntry in auditEntries)
                {
                    // Create log entity
                    var log = new ChangeLog
                    {
                        UserName = auditEntry.UserName,
                        IdTable = auditEntry.IdTable,
                        TableName = auditEntry.TableName,
                        Action = auditEntry.Action,
                        EntityName = auditEntry.EntityName,
                        IpAddress = auditEntry.IpAddress,
                        CreateAt = DateTime.UtcNow,
                        OldValues = JsonSerializer.Serialize(auditEntry.OldValues),
                        NewValues = JsonSerializer.Serialize(auditEntry.NewValues)
                    };
                    
                    logContext.ChangeLogs.Add(log);
                }

                await logContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log error but don't throw
                Console.WriteLine($"Error saving audit logs: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Crea un diccionario con los valores de las propiedades de una entidad.
        /// </summary>
        private Dictionary<string, object> CreateValuesDictionary(Microsoft.EntityFrameworkCore.ChangeTracking.PropertyValues propertyValues)
        {
            var dictionary = new Dictionary<string, object>();
            
            foreach (var property in propertyValues.Properties)
            {
                var value = propertyValues[property];
                dictionary[property.Name] = value ?? DBNull.Value;
            }
            
            return dictionary;
        }

        /// <summary>
        /// Ejecuta una consulta SQL utilizando Dapper y devuelve una colección de resultados de tipo genérico.
        /// </summary>
        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object? parameters = null, int? timeout = null, CommandType? type = null)
        {
           using var command = new DapperEFCoreCommand(this, text, parameters ?? new { }, timeout, type, CancellationToken.None);
           var connection = this.Database.GetDbConnection();
           return await connection.QueryAsync<T>(command.Definition);
        }

        /// <summary>
        /// Ejecuta una consulta SQL utilizando Dapper y devuelve un solo resultado o el valor predeterminado si no hay resultados.
        /// </summary>
        public async Task<T?> QueryFirstOrDefaultAsync<T>(string text, object? parameters = null, int? timeout = null, CommandType? type = null)
        {
           using var command = new DapperEFCoreCommand(this, text, parameters ?? new { }, timeout, type, CancellationToken.None);
           var connection = this.Database.GetDbConnection();
           return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }        
        
        /// <summary>
        /// Obtiene un IQueryable para usar en consultas LINQ que incluye filtro de status activo.
        /// </summary>
        public IQueryable<T> GetActiveSet<T>() where T : class
        {
            var query = Set<T>().AsQueryable();
            
            // Filtramos por Status aplicando expresiones genéricas si la entidad tiene la propiedad Status
            var parameter = Expression.Parameter(typeof(T), "e");
            
            if (typeof(T).GetProperty("Status") != null)
            {
                try {
                    // Construimos una expresión lambda para filtrar por Status = true
                    var property = Expression.Property(parameter, "Status");
                    var value = Expression.Constant(true);
                    var equal = Expression.Equal(property, value);
                    var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);
                    
                    // Aplicamos el filtro
                    query = query.Where(lambda);
                }
                catch {
                    // Si hay algún error, devolvemos el query sin filtrar
                }
            }
            
            return query;
        }

        /// <summary>
        /// Método auxiliar para obtener el valor de una propiedad de un objeto mediante reflexión.
        /// </summary>
        private static bool GetPropertyValue(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                return false;
            }
            return property.GetValue(obj, null) is bool value ? value : false;
        }
        
        /// <summary>
        /// Ejecuta una consulta con paginación utilizando LINQ.
        /// </summary>
        public IQueryable<T> GetPaged<T>(IQueryable<T> query, int page, int pageSize) where T : class
        {
            if (page <= 0)
                page = 1;
                
            if (pageSize <= 0)
                pageSize = 10;
                
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
         
        /// <summary>
        /// Ejecuta una consulta LINQ y devuelve los resultados como una colección asíncrona.
        /// </summary>
        public async Task<List<T>> ToListAsyncSafe<T>(IQueryable<T> query)
        {
            if (query == null)
                return new List<T>();
                
            return await EntityFrameworkQueryableExtensions.ToListAsync(query);
        }

        /// <summary>
        /// Método interno para garantizar la auditoría de los cambios en las entidades.
        /// </summary>
        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
            
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && 
                       (e.State == EntityState.Added || 
                        e.State == EntityState.Modified || 
                        e.State == EntityState.Deleted));

            var currentDateTime = DateTime.UtcNow;
            string userName = GetCurrentUserName();
            string ipAddress = GetCurrentIPAddress();
            var changeLogs = new List<ChangeLog>();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    // Preparamos los datos para el log
                    var tableName = entry.Entity.GetType().Name;
                    var primaryKey = "Id";
                    var keyValue = entry.Property(primaryKey).CurrentValue;
                    
                    var changeLog = new ChangeLog
                    {
                        TableName = tableName,
                        IdTable = keyValue != null ? Convert.ToInt32(keyValue) : 0,
                        UserName = userName,
                        EntityName = entry.Entity.GetType().FullName ?? tableName,
                        IpAddress = ipAddress,
                        CreateAt = currentDateTime
                    };

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            // Solo registramos la inserción en el log, sin modificar la entidad
                            changeLog.Action = "INSERT";
                            changeLog.NewValues = SerializeEntityToJson(entry.Entity);
                            changeLogs.Add(changeLog);
                            break;
                            
                        case EntityState.Modified:
                            // Solo registramos la actualización en el log, sin modificar la entidad
                            var oldValues = new Dictionary<string, object>();
                            var newValues = new Dictionary<string, object>();
                            
                            foreach (var property in entry.Properties.Where(p => p.IsModified))
                            {
                                oldValues[property.Metadata.Name] = property.OriginalValue ?? DBNull.Value;
                                newValues[property.Metadata.Name] = property.CurrentValue ?? DBNull.Value;
                            }
                            
                            if (oldValues.Count > 0)
                            {
                                changeLog.Action = "UPDATE";
                                changeLog.OldValues = System.Text.Json.JsonSerializer.Serialize(oldValues);
                                changeLog.NewValues = System.Text.Json.JsonSerializer.Serialize(newValues);
                                changeLogs.Add(changeLog);
                            }
                            break;
                            
                        case EntityState.Deleted:
                            // Convertimos el borrado en un borrado lógico (solo cambiamos Status)
                            entry.State = EntityState.Modified;
                            entity.Status = false;
                            
                            changeLog.Action = "DELETE";
                            changeLog.OldValues = SerializeEntityToJson(entry.Entity);
                            changeLogs.Add(changeLog);
                            break;
                    }
                }
            }
            
            // Guardar los logs en la base de datos de logs
            if (changeLogs.Count > 0)
            {
                SaveChangeLogs(changeLogs);
            }
        }

        /// <summary>
        /// Obtiene el nombre de usuario actual del contexto HTTP si está disponible
        /// </summary>
        private string GetCurrentUserName()
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext?.User?.Identity?.Name != null)
            {
                return _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            }
            return "System";
        }

        /// <summary>
        /// Obtiene la dirección IP del cliente actual si está disponible
        /// </summary>
        private string GetCurrentIPAddress()
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress != null)
            {
                return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// Serializa una entidad a formato JSON
        /// </summary>
        private string SerializeEntityToJson(object entity)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
            return JsonSerializer.Serialize(entity, options);
        }

        /// <summary>
        /// Guarda los registros de cambios en la base de datos de logs
        /// </summary>
        private void SaveChangeLogs(List<ChangeLog> changeLogs)
        {
            try
            {
                // Aquí deberíamos usar LogDbContext para guardar los logs
                using (var logContext = CreateLogDbContext())
                {
                    logContext.ChangeLogs.AddRange(changeLogs);
                    logContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Log the exception - no queremos que un error en el logging
                // afecte la operación principal
                Console.WriteLine($"Error al guardar logs: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea una instancia de LogDbContext
        /// </summary>
        private LogDbContext CreateLogDbContext()
        {
            // Obtener la cadena de conexión para los logs (podría ser la misma o diferente)
            var connectionString = _configuration.GetConnectionString("LogConnection");
            
            // Si no hay una conexión específica para logs, usar la misma de la aplicación
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _configuration.GetConnectionString("DefaultConnection");
            }

            var optionsBuilder = new DbContextOptionsBuilder<LogDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            
            return new LogDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Estructura para ejecutar comandos SQL con Dapper en Entity Framework Core.
        /// </summary>
        public readonly struct DapperEFCoreCommand : IDisposable
        {
            /// <summary>
            /// Constructor del comando Dapper.
            /// </summary>
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct
                );
            }

            /// <summary>
            /// Define los parámetros del comando SQL.
            /// </summary>
            public CommandDefinition Definition { get; }

            /// <summary>
            /// Método para liberar los recursos.
            /// </summary>
            public void Dispose()
            {
            }
        }
    }
}
