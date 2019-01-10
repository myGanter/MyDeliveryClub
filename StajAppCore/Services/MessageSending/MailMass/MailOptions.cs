using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajAppCore.Services.MessageSending.MailMass
{
    public class MailOptions
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public string SmtClient { get; set; }
        public int Port { get; set; }
    }
}
