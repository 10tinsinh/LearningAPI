using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunningMan.Models;
using RunningMan.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExampleController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserExampleController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        public IActionResult CreateUser(UserModelAllField user)
        {
            try
            {
                var _user = _userRepository.CreateUser(user);
                if (_user != null)
                {
                    return Ok(_user);

                }
                return BadRequest(new ApiResponse {
                    Success = false,
                    Message = "Username had exist"
                
                });

            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(UserModel user)
        {

            return Ok(_userRepository.Login(user));
        }
    }
}
