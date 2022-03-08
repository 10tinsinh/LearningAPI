using RunningMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Sevices
{
    public interface IUserRepository
    {
        UserId CreateUser(UserModelAllField user);
        ApiResponse Login(UserModel user);

        List<UserId> GetUses(string search);
    }
}
