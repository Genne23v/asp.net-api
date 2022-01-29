using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreCalifornia.DTOs
{
    public class AppDto
    {
        public string Name { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}