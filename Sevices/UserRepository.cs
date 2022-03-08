using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RunningMan.Data;
using RunningMan.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RunningMan.Sevices
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _Context;
        private readonly AppSetting _appSetting;

        public UserRepository(MyDbContext dbContext, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _Context = dbContext;
            _appSetting = optionsMonitor.CurrentValue;
        }

        public UserId CreateUser(UserModelAllField user)
        {
            var check = _Context.Users.SingleOrDefault(u => u.Username == user.Username);
            if (check == null)
            {

                var _user = new User
                {
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    UserStatus = true
                };
                _Context.Add(_user);
                _Context.SaveChanges();
                return new UserId
                {
                    Id = _user.Id,
                    Username = _user.Username,
                    Password = _user.Password,
                    Email = _user.Email,
                    Fullname = _user.Fullname,
                    UserStatus = _user.UserStatus
                };
            }
            return null;
        }

        public List<UserId> GetUses(string search)
        {
            throw new NotImplementedException();
        }

        public ApiResponse Login(UserModel user)
        {
            var _user = _Context.Users.SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if(_user == null)
            {
                var result = new ApiResponse
                {
                    Success= false,
                    Message = "Username or Password invalid",
                    Data = null
                };
                return result;
                
            }

            else
            {
                var result = new ApiResponse
                {
                    Success = true,
                    Message = "Loggin successfully",
                    Data = GenerateToken(_user)
                };
                return result;
            }


        }
        private string GenerateToken (User user)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var jwtTokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Fullname),
                    new Claim(ClaimTypes.Email, user.Email),

                    new Claim("Username", user.Username),

                    new Claim("Id", user.Id.ToString()),




                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtToken.CreateToken(jwtTokenDescription);
            return jwtToken.WriteToken(token);
        }

    }
}
