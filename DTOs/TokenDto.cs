﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreCalifornia.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
