using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Response
{
    public class ProductRes
    {
        public string? ProductName { get; set; }

        public string Sku { get; set; } = null!;

        public int? Price { get; set; }

        public int? Stock { get; set; }

        public string? Category { get; set; }

        public int Id { get; set; }
        public bool? Success { get; set; }
        public string Message { get; set; }
    }
}