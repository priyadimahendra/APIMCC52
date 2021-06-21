﻿using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, string>
    {
        public UniversityRepository(MyContext context) : base(context)
        {
        }
    }
}
