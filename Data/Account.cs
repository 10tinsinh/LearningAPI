using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Data
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int Account_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public virtual ICollection<DetailAccount> DetailAccounts { get; set; }
    }
}
