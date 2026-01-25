using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Extension;

namespace Models.Request
{
    public class TodoReq : PageReq
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? StatusId { get; set; }

        public string? Owner { get; set; }
    }
}