using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EdPlatform.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        public UserService()
        {
            _unitOfWork = new();
        }

        public async Task<UserModel?> Login(UserLoginModel user)
        {
            var dbUser = (await _unitOfWork.UserRepository.Find(u => u.Login == user.Login)).SingleOrDefault();
            if (dbUser == null)
                return null;

            var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password!,
                salt: Convert.FromBase64String(dbUser.PasswordSalt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
            );

            if (dbUser.HashPassword == passwordHash)
            {
                return new UserModel { UserId = dbUser.UserId, Email = dbUser.Email, Login = dbUser.Login };
            }

            return null;
        }

        public async Task Register(UserRegisterModel user)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
            );

            await _unitOfWork.UserRepository.Add(new User { 
                Login = user.Login, 
                Email = user.Email, 
                HashPassword = hashed, 
                PasswordSalt = Convert.ToBase64String(salt) 
            });
            await _unitOfWork.Save();
        }
    }
}
