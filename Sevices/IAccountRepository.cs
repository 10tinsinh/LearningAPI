using RunningMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Sevices
{
    public interface IAccountRepository
    {
        List<AccountModelAll> GetAll();

        List<AccountModelAll> GetAllAccount(string search, string sortBy);

        AccountModel GetById(int id);

        AccountModelAll Add(AccountModel account);
        AccountModel Login(AccountModel account);

        void Update(int id, AccountModel account);

        void Delete(int id);
    }
}
