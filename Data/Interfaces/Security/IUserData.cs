using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model;

namespace Data.Interfaces
{
    public interface IUserData : IBaseModelData<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> ValidateCredentialsAsync(string email, string password);
        Task<User> GetUserWithDetailsAsync(int id);
    }
}
