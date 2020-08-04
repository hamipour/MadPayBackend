
using MadPay724.Data.Models;
using MadPay724.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Repository.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsExist(string username);
       
    }
}
