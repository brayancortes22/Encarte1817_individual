using Business.Interfaces;
using Entity.Dtos.UserDTO;
using Entity.Model;

namespace Business.Interfaces
{
    ///<summary>
    /// Define los m�todos de negocio espec�ficos para la gest�on de usuarios.
    ///Hereda operaciones CRUD gen�ricas de <see cref="IBaseBusiness{User, UserDto}"/>.
    ///</summary>
    public interface IUserBusiness : IBaseBusiness<User, UserDto>
    {
        /// <summary>
        /// Obtiene un usuario por su direcci�n de correo electr�nico.
        /// </summary>
        /// <param name="email">Correo electr�nico del usuario a buscar.</param>
        /// <returns>
        /// El <see cref="User"/> que coincida con el correo proporcionado o <c>null</c> si no se encuentra.
        /// </returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Valida las credenciales de un usuario comparando el correo y la contrase�a.
        /// </summary>
        /// <param name="email">Correo electr�nico del usuario.</param>
        /// <param name="password">Contrase�a en texto plano para validar.</param>
        ///<returns>True si las credenciales son v�lidas; de lo contario false</returns>
        Task<bool> ValidateCredentialsAsync(string email, string password);

        /// <summary>
        /// Actualiza parcialmente los datos de un usuario.
        /// </summary>
        /// <param name="dto">Objeto que contiene los datos parciales a actualizar del usuario.</param>
        ///<returns>True si la actualizaci�n fue exitosa; de lo contario false</returns>
        Task<bool> UpdateParcialUserAsync(UpdateUserDto dto);

        /// <summary>
        /// Cambia el estado activo/inactivo de un usuario.
        /// </summary>
        /// <param name="dto">Objeto que contiene el ID del usuario y el nuevo estado activo.</param>
        ///<returns>True si el cambio de estado fue exitoso; de lo contario false</returns>

        Task<bool> SetUserActiveAsync(DeleteLogicalUserDto dto);


        ///<summary>
        /// Autentica un usuario en el sistema usando su corre electr�nico y contrase�a.
        //</summary>
        /// <param name="email">Correo electr�nico del usuario.</param>
        /// <param name="password">Contrase�a del usuario.</param>
        /// <returns>Objeto <see cref="User"/> si las credenciales son v�lidas; de lo contrario, null.</returns>
        Task<User> LoginAsync(string email, string password);

        ///<summary>
        /// Cambia la contrase�a de un usuario existente
        ///</summary>
        /// <param name="dto">Objeto que contiene el ID del usuario, la contrase�a actual y la nueva contrase�a.</param>
        /// <returns> True si la contrase�a se cambi� correctamente; false si el usuario no existe o la contrase�a actual no coincide.</returns>
        Task<bool> ChangePasswordAsync(UpdatePasswordUserDto dto);

        ///<summary>
        /// Asigna un rol a un usuario espec�fico.
        ///</summary>
        /// <param name="dto">Objeto que contiene el ID del usuario y el ID del rol a asignar.</param>
        /// <returns> True si el rol fue asignado correctamente; false si el usuario o el rol no existen. </returns>
        Task<bool> AssignRolAsync(AssignUserRolDto dto);

        /// <summary>
        /// Notifica al usuario mediante correo electr�nico sobre la creaci�n de su cuenta.
        /// </summary>
        /// <param name="emailDestino">Direcci�n de correo electr�nico del destinatario.</param>
        /// <param name="nombre">Nombre del usuario para personalizar el mensaje.</param>
        /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
        Task NotificarUsuarioAsync(string emailDestino, string nombre);

        /// <summary>
        /// Env�a un correo electr�nico con un enlace para restablecer la contrase�a del usuario.
        /// </summary>
        /// <param name="email">Direcci�n de correo electr�nico del usuario que solicita recuperar su contrase�a.</param>
        /// <returns>Una tarea que representa la operaci�n as�ncrona.</returns>
        Task EnviarCorreoRecuperacionAsync(string email);
        Task<bool> ChangePasswordAsync(int userId, string hashedPassword);
    }
}
    