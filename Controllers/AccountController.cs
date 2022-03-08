using Microsoft.AspNetCore.Authorization;
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
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet/*("{search}")*/]
        public IActionResult GetAllAccount(string search, string sortBy)
        {
            try
            {
                var result = _accountRepository.GetAllAccount(search, sortBy);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

     
        [HttpGet("{id}")]
        public IActionResult getbyid(int id)
        {
            try
            {
                var data = _accountRepository.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateAccount(AccountModel account)
        {
            try
            {
                return Ok(_accountRepository.Add(account));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, AccountModel account)
        {
            try
            {
                _accountRepository.Update(id, account);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                _accountRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

     
    }
}
