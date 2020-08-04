using MadPay724.Common.Helpers;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Models;
using MadPay724.Repository.Infrastructure;
using MadPay724.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        public AuthService(IUnitOfWork<MadPayDbContext> db)
        {
            _db = db;
        }

        /// <summary>
        /// برای احراز هویت و ورود کاربر
        /// </summary>
        /// <param name="username"> نام کاربری</param>
        /// <param name="password">کلمه عبور</param>
        /// <returns>an instanse of user</returns>
        public async Task<User> Login(string username, string password)
        {
            var user = await _db.UserRepository.GetAsync(p => p.UserName == username);
            if (user == null)
                return null;

            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Signup(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordHelper.GeneratePassword(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _db.UserRepository.InsertAsync(user);
            await _db.SaveAsync();
            return user;
        }
    }
}
