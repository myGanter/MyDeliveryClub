using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Models
{
    public class MenuItem
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public IEnumerable<MenuItem> LowMenuItem { get; set; }
    }
}
