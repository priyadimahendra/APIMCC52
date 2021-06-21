using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("tb_T_Account")]
    public class Account
    {
        //PK dan FK
        [Key]
        public string NIK { get; set; }
        public string Password { get; set; }

        public virtual Profiling Profiling { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
