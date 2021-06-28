using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("tb_M_University")]
    public class University
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }
}
