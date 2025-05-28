using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Entity.Context;
//datas
using Data.Implements.BaseData;
using Data.Interfaces;
using Data.Interfaces.Security;
using Data.Implements;

//business
using Business.Interfaces;
using Business.Implements;


//utilities
using Utilities.Interfaces;
using Utilities.Helpers;
using Utilities.Mail;
using Utilities.Jwt;

//web
using Web.ServiceExtension;
using FluentValidation;
using FluentValidation.AspNetCore;
using Business.Services;
using System.Text.Json;



var builder = WebApplication.CreateBuilder(args);

// Add controllers with FluentValidation
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining(typeof(Program));

// Add Swagger documentation using extension method
builder.Services.AddSwaggerDocumentation();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add LogDbContext (separate database for logs)
builder.Services.AddDbContext<LogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LogConnection")));

// Configure email service
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


// Configure JWT and Authentication Services
builder.Services.AddScoped<IJwtGenerator, GenerateTokenJwt>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Register global app settings
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

// Register generic repositories and business logic services
builder.Services.AddScoped(typeof(IBaseModelData<>), typeof(BaseModelData<>));

builder.Services.AddScoped(typeof(IBaseBusiness<,>), typeof(BaseBusiness<,>));

// Register User-specific services
builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();

// Register Role-specific services
builder.Services.AddScoped<IRolData, RolData>();
builder.Services.AddScoped<IRolBusiness, RolBusiness>();

// Register Authentication services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Register RoleUser-specific services
builder.Services.AddScoped<IRolUserData, RolUserData>();
builder.Services.AddScoped<IRoleUserBusiness, RoleUserBusiness>();

// Register Form services
builder.Services.AddScoped<IFormData, FormData>();
builder.Services.AddScoped<IFormBusiness, FormBusiness>();

// Register Form-Module relationship services
builder.Services.AddScoped<IFormModuleData, FormModuleData>();
builder.Services.AddScoped<IFormModuleBusiness, FormModuleBusiness>();

// Register RolFormPermission services
builder.Services.AddScoped<IRolFormPermissionData, RolFormPermissionData>();
builder.Services.AddScoped<IRolFormPermissionBusiness, RolFormPermissionBusiness>();


// Register ModulePermission services
builder.Services.AddScoped<IModulePermissionData, ModulePermissionData>();
builder.Services.AddScoped<IModulePermissionBusiness, ModulePermissionBusiness>();

// Register Module services
builder.Services.AddScoped<IModuleData, ModuleData>();
builder.Services.AddScoped<IModuleBusiness, ModuleBusiness>();

// Register Permission services
builder.Services.AddScoped<IPermissionData, PermissionData>();
builder.Services.AddScoped<IPermissionBusiness, PermissionBusiness>();

// Register Person services
builder.Services.AddScoped<IPersonData, PersonData>();
builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();

// Register Location services
builder.Services.AddScoped<ICountryData, CountryData>();
builder.Services.AddScoped<ICountryBusiness, CountryBusiness>();

builder.Services.AddScoped<IDepartmentData, DepartmentData>();
builder.Services.AddScoped<IDepartmentBusiness, DepartmentBusiness>();

builder.Services.AddScoped<ICityData, CityData>();
builder.Services.AddScoped<ICityBusiness, CityBusiness>();

builder.Services.AddScoped<IDistrictData, DistrictData>();
builder.Services.AddScoped<IDistrictBusiness, DistrictBusiness>();

// Register Person-related services
builder.Services.AddScoped<IClientData, ClientData>();
builder.Services.AddScoped<IClientBusiness, ClientBusiness>();

builder.Services.AddScoped<IEmployeeData, EmployeeData>();
builder.Services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();

builder.Services.AddScoped<IProviderData, ProviderData>();
builder.Services.AddScoped<IProviderBusiness, ProviderBusiness>();

// Register utility helpers
builder.Services.AddScoped<IGenericIHelpers, GenericHelpers>();
builder.Services.AddScoped<IDatetimeHelper, DatetimeHelper>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IAuthHeaderHelper, AuthHeaderHelper>();
builder.Services.AddScoped<IRoleHelper, RoleHelper>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IValidationHelper, ValidationHelper>();
builder.Services.AddHttpContextAccessor();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // Primero intentamos usar la configuración desde AppSettings
    var jwtKey = builder.Configuration["AppSettings:JwtSecretKey"] ?? 
                 builder.Configuration["JWT:Key"] ?? 
                 throw new InvalidOperationException("JWT key is not configured");
    
    var key = Encoding.UTF8.GetBytes(jwtKey);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Append("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(";");
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(optionsCORS =>
        {
            optionsCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader();
        });
    });

    // Register AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Sistema de Gestión v1");
            c.RoutePrefix = string.Empty; // Para servir Swagger UI en la raíz
        });
    }    // Use custom exception handling middleware
    app.UseExceptionHandler(appError =>
    {
        appError.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorJson = System.Text.Json.JsonSerializer.Serialize(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error interno del servidor."
            });
            
            await context.Response.WriteAsync(errorJson);
        });
    });

    // Enable CORS
    app.UseCors();

    app.UseHttpsRedirection();

    // Add authentication & authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();    // Inicializar base de datos y aplicar migraciones para la base de datos principal
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            // Aplicar migraciones para la base de datos principal
            dbContext.Database.Migrate();
            logger.LogInformation("Base de datos principal verificada y migraciones aplicadas exitosamente.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocurrió un error durante la migración de la base de datos principal.");
        }
        
        // Inicializar y aplicar migraciones para la base de datos de logs
        try
        {
            var logDbContext = services.GetRequiredService<LogDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            // Aplica migraciones para la base de datos de logs
            // Asegúrate de tener migraciones para LogDbContext o usa EnsureCreated en vez de Migrate
            logDbContext.Database.Migrate();
            logger.LogInformation("Base de datos de logs verificada y creada exitosamente.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocurrió un error durante la creación de la base de datos de logs.");
        }
    }

    app.Run();


}