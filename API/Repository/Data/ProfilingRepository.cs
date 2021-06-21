using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        public ProfilingRepository(MyContext context) : base(context)
        {
        }
    }
}
