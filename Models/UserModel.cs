using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Models
{
    public class UserModel
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }

    public class UserModelAllField:UserModel
    {
        [Required]
        [MaxLength(100)]
        public string Fullname { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        public bool UserStatus { get; set; }
    }
    public class UserId:UserModelAllField
    {
        public int Id { get; set; }
    }
}
