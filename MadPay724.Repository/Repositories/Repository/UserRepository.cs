
using System.Threading.Tasks;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Models;
using MadPay724.Repository.Infrastructure;
using MadPay724.Repository.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MadPay724.Repository.Repositories.Repository
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private readonly DbContext _db;

        public UserRepository(DbContext db): base(db)
        {
            _db = (_db ?? (MadPayDbContext)_db);
        }

        public async Task<bool> IsExist(string username)
        {
            if (await GetAsync(p => p.UserName == username) != null)
                return true;

            return false;
        }

    }
}
