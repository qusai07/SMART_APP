﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMART_APP.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
    }
}
