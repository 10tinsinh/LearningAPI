using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Data
{
    [Table("DetailAccount")]
    public class DetailAccount
    {
        [Key]
        public int DetailAccount_id { get; set; }
        public int? Account_id { get; set; }
        public int? AccountType_id { get; set; }
        
        [ForeignKey("Account_id")]
        public Account Account { get; set; }

        [ForeignKey("AccountType_id")]
        public AccountType AccountType { get; set; }
    }
}
