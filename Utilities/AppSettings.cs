namespace Utilities
{
    /// <summary>
    /// Configuraciones globales de la aplicación que se cargan desde appSettings.json
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Clave secreta para la generación de tokens JWT
        /// </summary>
        public string JwtSecretKey { get; set; }
        
        /// <summary>
        /// Tiempo de expiración del token JWT en minutos
        /// </summary>
        public int JwtExpirationMinutes { get; set; }
        
        /// <summary>
        /// URL base de la aplicación (para los enlaces en emails)
        /// </summary>
        public string AppUrl { get; set; }
        
        /// <summary>
        /// Configuración para el servicio de correo electrónico
        /// </summary>
        public MailSettings MailSettings { get; set; }
        public object ResetPasswordBaseUrl { get; set; }
    }
    
    /// <summary>
    /// Configuraciones para el servicio de correo electrónico
    /// </summary>
    public class MailSettings
    {
        /// <summary>
        /// Servidor SMTP
        /// </summary>
        public string SmtpServer { get; set; }
        
        /// <summary>
        /// Puerto del servidor SMTP
        /// </summary>
        public int SmtpPort { get; set; }
        
        /// <summary>
        /// Email del remitente
        /// </summary>
        public string SenderEmail { get; set; }
        
        /// <summary>
        /// Nombre del remitente
        /// </summary>
        public string SenderName { get; set; }
        
        /// <summary>
        /// Usuario para autenticación SMTP
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Contraseña para autenticación SMTP
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Indica si se debe usar SSL
        /// </summary>
        public bool EnableSsl { get; set; }
    }
}
