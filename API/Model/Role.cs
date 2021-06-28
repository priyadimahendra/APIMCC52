using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("tb_M_Role")]
    public class Role
    {
        public string Id { get; set; }
        public string RoleName { get; set; }

        [JsonIgnore]
        public virtual List<AccountRole> AccountRoles { get; set; }
    }
}
