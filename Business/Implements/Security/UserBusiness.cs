using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces.Security;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utilities.Interfaces;
using Utilities.Mail;
using ValidationException = Utilities.Exceptions.ValidationException;
using Entity.Dtos;
namespace Business.Implements
{
    /// <summary>
    /// Implementación de la lógica de negocio para la gestión de usuarios.
    /// Extiende BaseBusiness heredando la lógica de negocio de los métodos base CRUD.
    /// </summary>
    public class UserBusiness : BaseBusiness<User, UserDto>, IUserBusiness
    {
        private readonly IUserData _userData;
        private readonly IEmailService _emailService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly AppSettings _appSettings;
        
        /// <summary>
        /// Constructor que inicializa las dependencias necesarias para la gestión de usuarios.
        /// </summary>
        public UserBusiness(
            IUserData userData,
            IMapper mapper,
            ILogger<UserBusiness> logger,
            IGenericIHelpers helpers,
            IEmailService emailService,
            IJwtGenerator jwtGenerator,
            IOptions<AppSettings> appSettings)
            : base(userData, mapper, logger, helpers)
        {
            _userData = userData;
            _emailService = emailService;
            _jwtGenerator = jwtGenerator;
            _appSettings = appSettings.Value;
        }

        
    }
}