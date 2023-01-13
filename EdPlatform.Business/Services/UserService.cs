using EdPlatform.Business.Models;
using EdPlatform.Data;
using EdPlatform.Data.Entities;
using EdPlatform.Data.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.UserRepository.Add(new User
            {
                Login = user.Login,
                Email = user.Email,
                HashPassword = hashed,
                PasswordSalt = Convert.ToBase64String(salt)
            });
            await _unitOfWork.Save();
        }

        public async Task<UserModel?> GetUser(string? login)
        {
            var dbUser = (await _unitOfWork.UserRepository.Find(u => u.Login == login)).SingleOrDefault();
            if (dbUser == null)
                return null;

            return new UserModel() { UserId = dbUser.UserId, Email = dbUser.Email, Login = dbUser.Login };
        }

        public async Task UpdateUser(UserModel user)
        {
            User dbUser;

            dbUser = await _unitOfWork.UserRepository.Get(user.UserId);
            _unitOfWork.UserRepository.Update(new User()
            {
                UserId = user.UserId,
                Login = user.Login,
                Email = user.Email,
                HashPassword = dbUser.HashPassword,
                PasswordSalt = dbUser.PasswordSalt
            });

            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<UserModel>> GetAllByLogin(string login)
        {
            List<UserModel> results = new();

            foreach (var user in (await _unitOfWork.UserRepository.Find(x => x.Login == login)))
            {
                results.Add(new UserModel() { Login = user.Login, Email = user.Email, UserId = user.UserId });
            }

            return results;
        }
    }
}
