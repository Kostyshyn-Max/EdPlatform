using EdPlatform.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Service
{
    public interface IUserService
    {
       Task Register(UserRegisterModel user);
       Task<UserModel?> Login(UserLoginModel user);
    }
}
