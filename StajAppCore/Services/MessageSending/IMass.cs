using System.Threading.Tasks;

namespace StajAppCore.Services.MessageSending
{
    public interface IMass
    {
        Task SendMessage(string to, string header, string message);
    }
}
