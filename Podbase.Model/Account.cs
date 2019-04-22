﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace Podbase.Model
{
    public class Account
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { private get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
