using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Data
{
    [Table("AccountType")]
    public class AccountType
    {
        [Key]
        public int AccountType_id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name_type { get; set; }

        public virtual ICollection<DetailAccount> DetailAccounts { get; set; }
    }
}
