using System;
using MimeKit;
using System.IO;
using MailKit.Net.Smtp;
using StajAppCore.Models;
using System.Threading.Tasks;

namespace StajAppCore.Services.MessageSending.MailMass
{
    public class MailMassService : IMass
    {
        public MailOptions Options { get; }

        private static string mailPage = null;
        public static string MailPagePath
        {
            set => mailPage = File.ReadAllText(value);    
        }

        private ExceptionDBLog ExDb;

        public MailMassService(MailOptions options, ExceptionDBLog exDb)
        {            
            Options = options;
            ExDb = exDb;
        }        

        public Task SendMessage(string to, string header, string message) => Task.Run(async () =>
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(Options.Name, Options.Email));
                emailMessage.To.Add(new MailboxAddress("", to));
                emailMessage.Subject = header;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = string.Format(mailPage, message)
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(Options.SmtClient, Options.Port, false);
                    await client.AuthenticateAsync(Options.Email, Options.Passwd);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception ex)
            {
                ExDb.OnException(new DBEroorModel()
                {
                    Exception = ex.Message,
                    StackTrace = ex.StackTrace,
                    Data = DateTime.Now                    
                });
            } 
        });
    }
}
