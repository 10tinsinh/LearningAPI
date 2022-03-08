using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options): base(options) { }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> accountTypes { get; set; }

        public DbSet<DetailAccount> DetailAccounts { get; set; }
    }
}
