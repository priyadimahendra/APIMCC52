﻿using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, string>
    {
        public RoleRepository(MyContext context) : base(context)
        {
        }
    }
}
