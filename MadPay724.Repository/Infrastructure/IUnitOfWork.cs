using MadPay724.Repository.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MadPay724.Repository.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext: DbContext
    {
        IUserRepository UserRepository { get; }
        void Save();
        Task<int> SaveAsync();
    }
}
