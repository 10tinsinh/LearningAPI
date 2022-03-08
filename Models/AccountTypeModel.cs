using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunningMan.Models
{
    public class AccountTypeModel
    {
        [Required]
        [MaxLength(50)]
        public string Name_type { get; set; }
    }
}
