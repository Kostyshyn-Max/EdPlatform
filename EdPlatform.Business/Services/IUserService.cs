using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public interface IUserService
    {
        Task Register(UserRegisterModel user);
        Task<UserModel?> Login(UserLoginModel user);
        Task<UserModel?> GetUser(string? login);
        Task UpdateUser(UserModel user);
        Task<IEnumerable<UserModel>> GetAllByLogin(string login);
    }
}
