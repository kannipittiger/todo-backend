using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Response
{
    public class Response
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}