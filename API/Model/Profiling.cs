using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    [Table("tb_T_Profiling")]
    public class Profiling
    {
        //PK dan FK
        [Key]
        public string NIK { get; set; }
        public string EducationId { get; set; }

        public virtual Education Education { get; set; }
        public virtual Account Account { get; set; }
    }
}
