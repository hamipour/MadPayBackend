using MadPay724.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Services.Interface
{
    public interface IAuthService
    {
        Task<User> Signup(User user, string password);
        /// <summary>
        /// برای احراز هویت و ورود کاربر
        /// </summary>
        /// <param name="username"> نام کاربری</param>
        /// <param name="password">کلمه عبور</param>
        /// <returns>an instanse of user</returns>
        Task<User> Login(string username, string password);

    }
}
