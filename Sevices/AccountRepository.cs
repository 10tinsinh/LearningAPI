using RunningMan.Data;
using RunningMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Sevices
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;

        public AccountRepository(MyDbContext context)
        {
            _context = context;

        }

        public AccountRepository()
        {
        }

        //public AccountModel Logins(AccountModel account)
        //{
        //    var user = _context.Accounts.SingleOrDefault(ac => ac.Username == account.Username && ac.Password == account.Password);
        //    if (user == null)
        //    {
        //        return new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Invalid username/password"

        //        };
        //    }
        //}
        public AccountModelAll Add(AccountModel account)
        {
            var _account = new Account
            {
                Username = account.Username,
                Password = account.Password
            };
            _context.Add(_account);
            _context.SaveChanges();
            return new AccountModelAll
            {
                Account_id = _account.Account_id,
                Username = _account.Username,
                Password = _account.Password
            };
        }

        public void Delete(int id)
        {
            var _account = _context.Accounts.SingleOrDefault(ac => ac.Account_id == id);
            if(_account != null)
            {
                _context.Remove(_account);
                _context.SaveChanges();
            }    
        }

        public List<AccountModelAll> GetAll()
        {
            var _account = _context.Accounts.Select(ac => new AccountModelAll
            {
                Account_id = ac.Account_id,
                Username = ac.Username,
                Password = ac.Password
            });
            return _account.ToList();
 
        }

        public List<AccountModelAll> GetAllAccount(string search, string sortBy)
        {

            var _account = _context.Accounts.AsQueryable();
            if(!string.IsNullOrEmpty(search))
            {
                _account = _account.Where(ac => ac.Username.Contains(search));
            }

            _account = _account.OrderBy(ac => ac.Username);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "desc": _account = _account.OrderByDescending(ac => ac.Username); break;
                    case "asc": _account = _account.OrderBy(ac => ac.Username); break;
                }    
            }    

            var data = _account.Select(ac => new AccountModelAll
            {
                Account_id = ac.Account_id,
                Username = ac.Username,
                Password = ac.Password

            });
            return data.ToList();
        }

        public AccountModel GetById(int id)
        {
            var _account = _context.Accounts.SingleOrDefault(ac => ac.Account_id == id);
            if(_account != null)
            {
                return new AccountModel
                {
                    Username = _account.Username,
                    Password = _account.Password
                };
            }
            return null;
        }

        public void Update(int id, AccountModel account)
        {
            var _account = _context.Accounts.SingleOrDefault(ac => ac.Account_id == id);
            if(_account != null)
            {
                _account.Username = account.Username;
                _account.Password = account.Password;
                _context.SaveChanges();

            };
        }

        public AccountModel Login(AccountModel account)
        {
            throw new NotImplementedException();
        }
    }
}
