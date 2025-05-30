using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Business.Interfaces;
using Data.Interfaces;
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
        
        /// <summary>
        /// Obtiene un usuario por su email
        /// </summary>
        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userData.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Valida las credenciales de un usuario
        /// </summary>
        public async Task<UserDto> ValidateCredentialsAsync(string email, string password)
        {
            // Convertir la contraseña a hash antes de validar
            string hashedPassword = ConvertToHash(password);
            var user = await _userData.ValidateCredentialsAsync(email, hashedPassword);
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Obtiene un usuario con todos sus detalles relacionados
        /// </summary>
        public async Task<UserDto> GetUserWithDetailsAsync(int id)
        {
            var user = await _userData.GetUserWithDetailsAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Autentica a un usuario y genera un token JWT
        /// </summary>
        public async Task<string> AuthenticateAsync(LoginRequestDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new ValidationException("El email y la contraseña son obligatorios");
            }

            var user = await _userData.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new ValidationException("Usuario no encontrado");
            }

            // Verificar la contraseña
            string hashedPassword = ConvertToHash(loginDto.Password);
            if (user.Password != hashedPassword)
            {
                throw new ValidationException("Credenciales incorrectas");
            }            // Generar token JWT
            var authDto = await _jwtGenerator.GeneradorToken(user);
            return authDto.Token;
        }

        /// <summary>
        /// Actualiza la contraseña de un usuario
        /// </summary>
        public async Task<UserDto> UpdatePasswordAsync(int id, UpdatePasswordUserDto passwordDto)
        {
            var user = await _userData.GetByIdAsync(id);
            if (user == null)
            {
                throw new ValidationException("Usuario no encontrado");
            }

            // Verificar la contraseña actual
            string hashedCurrentPassword = ConvertToHash(passwordDto.CurrentPassword);
            if (user.Password != hashedCurrentPassword)
            {
                throw new ValidationException("La contraseña actual es incorrecta");
            }

            // Actualizar la contraseña
            user.Password = ConvertToHash(passwordDto.NewPassword);
            await _userData.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Convierte una cadena a un hash MD5
        /// </summary>
        private string ConvertToHash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}