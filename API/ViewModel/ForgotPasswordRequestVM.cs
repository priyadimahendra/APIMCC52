﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class ForgotPasswordRequestVM
    {
        public string Email { get; set; }
        public string NIK { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
