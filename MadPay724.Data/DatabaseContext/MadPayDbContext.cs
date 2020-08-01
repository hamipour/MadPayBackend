using MadPay724.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay724.Data.DatabaseContext
{
    public class MadPayDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=MadPayDb; Integrated Security=true;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
    }
}
