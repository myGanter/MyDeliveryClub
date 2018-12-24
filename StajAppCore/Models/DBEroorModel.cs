using System;

namespace StajAppCore.Models
{
    public class DBEroorModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Data { get; set; }

        public string Exception { get; set; }

        public string StackTrace { get; set; }

        public string Url { get; set; }
    }
}
