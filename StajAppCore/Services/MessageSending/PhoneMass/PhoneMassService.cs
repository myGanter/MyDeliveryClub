using System;
using System.Threading.Tasks;

namespace StajAppCore.Services.MessageSending.PhoneMass
{
    public class PhoneMassService : IMass
    {
        public Task SendMessage(string to, string header, string message)
        {
            throw new NotImplementedException();
            //отправка cmc
        }
    }
}
