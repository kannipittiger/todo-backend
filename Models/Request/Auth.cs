using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Request
{
    public class Auth
    {
        public String? User { get; set; }
        public String? Password { get; set; }
    }
}