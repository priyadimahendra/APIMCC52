using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
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

        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        public virtual List<AccountRole> AccountRoles { get; set; }
    }
}
