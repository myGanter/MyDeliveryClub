using System.Collections.Generic;

namespace StajAppCore.Models
{
    public class MenuItem
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public IEnumerable<MenuItem> LowMenuItem { get; set; }
    }
}
